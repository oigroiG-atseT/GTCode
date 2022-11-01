using GTCode.Generators.Utils;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;
using Microsoft.CodeAnalysis;
using System.Text;
using System.Text.RegularExpressions;

namespace GTCode.Generators.MVVM.CommunityToolkit.Wrappers
{
    [Obsolete("Classe deprecata e mai completamente sviluppata. Non rimuovere in quanto contiene forme valide.", false)]
    [Generator]
    public class ObservableClassWrapperGenerator : ISourceGenerator
    {
        private readonly string ATTRIBUTE_TEMPLATE = AttributeDefinitions.ObservableClassWrapperAttribute();

        public void Execute(GeneratorExecutionContext context)
        {
#if DEBUG
            if (context.SyntaxContextReceiver is not MainSyntaxReceiver receiver) return;
            if (receiver.ClassSyntaxHelper is null) return;

            var classSyntaxHelper = this.AddGlobalReferenceToClassSyntaxHelper(context, receiver);
            if (classSyntaxHelper is null) return;

            var classBuilder = this.GenerateClass(classSyntaxHelper);
            this.GenerateConstructor(classSyntaxHelper, ref classBuilder);
            this.GenerateGetCore(classSyntaxHelper, ref classBuilder);
            this.CloseClass(ref classBuilder);

            context.AddSource($"{receiver.ClassSyntaxHelper.Name}_observableClassWrapper.g.cs", SourceText.From(classBuilder.ToString(), Encoding.UTF8));
#endif
        }

        public void Initialize(GeneratorInitializationContext context)
        {
#if DEBUG
            context.RegisterForPostInitialization((i) => i.AddSource("ObservableClassWrapperAttribute.g.cs", ATTRIBUTE_TEMPLATE));
            context.RegisterForSyntaxNotifications(() => new MainSyntaxReceiver());
#endif
        }

        #region METHODS

        private ClassSyntaxHelper AddGlobalReferenceToClassSyntaxHelper(GeneratorExecutionContext context, MainSyntaxReceiver receiver)
        {
            var classSyntaxHelper = receiver.ClassSyntaxHelper;

            var classesTree = context.Compilation.SyntaxTrees.Where(st => st.GetRoot().DescendantNodes().OfType<ClassDeclarationSyntax>().Any());
            var classes = classesTree.Where(t => t.GetRoot().DescendantNodes().OfType<ClassDeclarationSyntax>().Any());
            foreach (SyntaxTree tree in classes)
            {
                foreach (var declaredClass in tree
                    .GetRoot()
                    .DescendantNodes()
                    .OfType<ClassDeclarationSyntax>().Where(c => c.Identifier.Text.Equals(classSyntaxHelper.CoreItemType)))
                {
                    NamespaceDeclarationSyntax namespaceFieldDeclaration = null;
                    if (!SyntaxNodeUtils.TryGetParentSyntax(declaredClass, out namespaceFieldDeclaration)) return null;
                    classSyntaxHelper.CoreItemType = $"global::{namespaceFieldDeclaration.Name}.{classSyntaxHelper.CoreItemType}";
                }
            }
            return classSyntaxHelper;
        }

        private StringBuilder GenerateClass(ClassSyntaxHelper declaredClass)
        {
            var builder = new StringBuilder();

            builder.Append($@"
namespace {declaredClass.Namespace}
{{
    public partial class {declaredClass.Name} {{
               
");

            return builder;
        }

        private void GenerateConstructor(ClassSyntaxHelper declaredClass, ref StringBuilder builder)
        {
            builder.Append($@"
        public {declaredClass.Name}({declaredClass.CoreItemType} core) {{    
            this.{declaredClass.CoreItemName} = core;
");
            foreach (string field in declaredClass.Fields)
            {
                var propertyName = this.ChoosePropertyName(field);
                builder.AppendLine($"\t\t\tthis.{propertyName} = core.{propertyName};");
            }

            builder.Append(@"
        }
");
        }

        private void GenerateGetCore(ClassSyntaxHelper declaredClass, ref StringBuilder builder)
        {
            builder.Append($@"
        public {declaredClass.CoreItemType} GetCore() {{    
            var core = this.{declaredClass.CoreItemName};
");
            foreach (string field in declaredClass.Fields)
            {
                var propertyName = this.ChoosePropertyName(field);
                builder.AppendLine($"\t\t\tcore.{propertyName} = this.{propertyName};");
            }

            builder.Append(@"
            return core;
        }
");
        }

        private void CloseClass(ref StringBuilder generatedClass)
        {
            generatedClass.Append(@"
    }
}
");
        }

        /// <summary>
        /// Decide che nome attribuire alla Property:
        /// * _test diventa Test
        /// * test diventa Test
        /// </summary>        
        private string ChoosePropertyName(string fieldName)
        {
            fieldName = fieldName.TrimStart('_');
            if (fieldName.Length == 0) return string.Empty;
            if (fieldName.Length == 1) return fieldName.ToUpper();
            return fieldName.Substring(0, 1).ToUpper() + fieldName.Substring(1);
        }

        #endregion

        private class MainSyntaxReceiver : ISyntaxContextReceiver
        {
            public ClassSyntaxHelper ClassSyntaxHelper { get; set; }

            public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
            {
                if (context.Node is ClassDeclarationSyntax classDeclarationSyntax && classDeclarationSyntax.AttributeLists.Count > 0)
                {
                    ClassSyntaxHelper csh = new ClassSyntaxHelper();

                    var attributeString = classDeclarationSyntax.AttributeLists.FirstOrDefault(
                        attribute => attribute.ToFullString().Contains("ObservableClassWrapper") && !attribute.ToFullString().Contains("AttributeUsage")
                     )?.ToFullString();
                    var result = Regex.Match(attributeString ?? String.Empty, "(?<=\")(.*)(?=\")");
                    if (!result.Success) return;

                    NamespaceDeclarationSyntax namespaceDeclaration = null;
                    if (!SyntaxNodeUtils.TryGetParentSyntax(classDeclarationSyntax, out namespaceDeclaration)) return;

                    csh.Name = classDeclarationSyntax.Identifier.Text;
                    csh.Namespace = namespaceDeclaration.Name.ToString();
                    csh.CoreItemName = result.Value;

                    foreach (MemberDeclarationSyntax variable in classDeclarationSyntax.Members.Where(m => m.RawKind == (int)SyntaxKind.FieldDeclaration))
                    {
                        string fieldString = (variable as FieldDeclarationSyntax)?.Declaration.ToFullString();
                        string fieldName = (variable as FieldDeclarationSyntax)?.Declaration?.GetLastToken().Text;
                        if (fieldName is null) continue;
                        if (csh.CoreItemName is null) continue;
                        if (variable.AttributeLists.Any(a => a.ToFullString().Contains("ObservableProperty")))
                        {
                            csh.Fields.Add(fieldName);
                        }
                        if (fieldString.Contains(csh.CoreItemName))
                        {
                            string fieldTypeString = Regex.Match(fieldString, $@"\w+(?=\s+{csh.CoreItemName})").Value;
                            csh.CoreItemType = fieldTypeString;
                        }
                    }
                    this.ClassSyntaxHelper = csh;
                }
            }
        }

        private class ClassSyntaxHelper
        {

            public string Name { get; set; }
            public string Namespace { get; set; }
            public string CoreItemName { get; set; }
            public string CoreItemType { get; set; }
            public List<string> Fields { get; set; } = new List<string>();

        }

    }
}
