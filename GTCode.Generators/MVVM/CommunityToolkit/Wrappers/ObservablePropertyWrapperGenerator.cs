using GTCode.Generators.Utils;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using Microsoft.CodeAnalysis;
using System.Text;

namespace GTCode.Generators.MVVM.CommunityToolkit.Wrappers
{
    [Generator]
    public class ObservablePropertyWrapperGenerator : ISourceGenerator
    {

        private readonly string ATTRIBUTE_TEMPLATE = AttributeDefinitions.ObservablePropertyWrapperAttribute();
        private readonly string ATTRIBUTE_METADATA_NAME = AttributeDefinitions.ObservablePropertyWrapperAttributeMetadataName();
        private readonly string TAB_INDENT = Globals.TAB_INDENT;

        public void Execute(GeneratorExecutionContext context)
        {
            if (context.SyntaxContextReceiver is not MainSyntaxReceiver receiver) return;
            INamedTypeSymbol attributeSymbol = context.Compilation.GetTypeByMetadataName(ATTRIBUTE_METADATA_NAME);

            foreach (IGrouping<INamedTypeSymbol, IFieldSymbol> group in receiver.Fields.GroupBy<IFieldSymbol, INamedTypeSymbol>(f => f.ContainingType, SymbolEqualityComparer.Default))
            {
                string classSource = ProcessClass(group.Key, group.ToList(), attributeSymbol, context);
                context.AddSource($"{group.Key.Name}_observablePropertyWrapper.g.cs", SourceText.From(classSource, Encoding.UTF8));
            }
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForPostInitialization((i) => i.AddSource(AttributeDefinitions.ObservablePropertyWrapperAttributeName() + ".g.cs", ATTRIBUTE_TEMPLATE));
            context.RegisterForSyntaxNotifications(() => new MainSyntaxReceiver(ATTRIBUTE_METADATA_NAME));
        }

        #region METHODS
        private string ProcessClass(INamedTypeSymbol classSymbol, List<IFieldSymbol> fields, ISymbol attributeSymbol, GeneratorExecutionContext context)
        {
            if (!classSymbol.ContainingSymbol.Equals(classSymbol.ContainingNamespace, SymbolEqualityComparer.Default))
            {
                return null; //TODO: issue a diagnostic that it must be top level
            }

            string namespaceName = classSymbol.ContainingNamespace.ToDisplayString();

            StringBuilder source = new StringBuilder($@"
namespace {namespaceName}
{{
    public partial class {classSymbol.Name}
    {{
");

            foreach (IFieldSymbol fieldSymbol in fields)
            {
                ProcessField(source, fieldSymbol, attributeSymbol);
            }

            source.Append($"\n{TAB_INDENT}}}\n}}");
            return source.ToString();
        }

        /// <summary>
        /// Aggiunge allo StringBuilder la nuova Property in base agli attributi forniti
        /// </summary>    
        private void ProcessField(StringBuilder source, IFieldSymbol fieldSymbol, ISymbol attributeSymbol)
        {
            string fieldName = fieldSymbol.Name;
            ITypeSymbol fieldType = fieldSymbol.Type;

            AttributeData attributeData = fieldSymbol.GetAttributes().Single(ad => ad.AttributeClass.Equals(attributeSymbol, SymbolEqualityComparer.Default));

            string coreName = AttributeDataUtils.GetConstructorArgumentValue(attributeData, 0);
            string corePropertyChain = AttributeDataUtils.GetPropertyValue(attributeData, "CorePropertyChain");
            string overridenPropertyName = AttributeDataUtils.GetPropertyValue(attributeData, "PropertyName");
            string coreReference = String.Empty;

            string propertyName = ChoosePropertyName(fieldName);
            if (propertyName.Length == 0 || propertyName == fieldName)
            {
                return; //TODO: issue a diagnostic that we can't process this field
            }

            coreReference = propertyName;
            if (!overridenPropertyName.Equals(String.Empty)) propertyName = overridenPropertyName;
            if (!corePropertyChain.Equals(String.Empty))
            {
                coreReference = corePropertyChain;
                if (!overridenPropertyName.Equals(String.Empty))
                {
                    coreReference = corePropertyChain;
                    propertyName = overridenPropertyName;
                }
            }

            source.Append($@"
        public {fieldType} {propertyName} 
        {{
            get => {coreName}.{coreReference};    
            set {{
                {fieldName} = value;
                SetProperty({coreName}.{coreReference}, value, {coreName}, (i, v) => i.{coreReference} = v);                 
            }}
        }}
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
            public List<IFieldSymbol> Fields { get; } = new List<IFieldSymbol>();
            private string _findAttributeClass;

            public MainSyntaxReceiver(string findAttributeClass) { this._findAttributeClass = findAttributeClass; }

            /// <summary>
            /// Cerca le classi con metodi annotati con '_findAttributeClass' e ne ottiene le informazioni
            /// </summary>
            public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
            {
                if (context.Node is FieldDeclarationSyntax fieldDeclarationSyntax && fieldDeclarationSyntax.AttributeLists.Count > 0)
                {
                    foreach (VariableDeclaratorSyntax variable in fieldDeclarationSyntax.Declaration.Variables)
                    {
                        IFieldSymbol fieldSymbol = context.SemanticModel.GetDeclaredSymbol(variable) as IFieldSymbol;
                        if (fieldSymbol.GetAttributes().Any(ad => ad.AttributeClass.ToDisplayString() == _findAttributeClass))
                        {
                            Fields.Add(fieldSymbol);
                        }
                    }
                }
            }
        }

    }
}
