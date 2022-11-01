using Microsoft.CodeAnalysis;

namespace GTCode.Generators.Utils
{
    internal class SyntaxNodeUtils
    {

        /// <summary>
        /// Restituisce tramite out result il SyntaxNode del tipo result, se disponibile.
        /// </summary>
        /// <example>
        /// NamespaceDeclarationSyntax namespaceDeclarationSyntax = null;
        /// SyntaxNodeHelper.TryGetParentSyntax(classDeclarationSyntax, out namespaceDeclarationSyntax);
        /// </example>        
        public static bool TryGetParentSyntax<T>(SyntaxNode syntaxNode, out T result) where T : SyntaxNode
        {
            result = null;

            if (syntaxNode == null) return false;

            try
            {
                syntaxNode = syntaxNode.Parent;

                if (syntaxNode == null) { return false; }
                if (syntaxNode.GetType() == typeof(T))
                {
                    result = syntaxNode as T;
                    return true;
                }

                return TryGetParentSyntax(syntaxNode, out result);
            }
            catch
            {
                return false;
            }
        }

    }
}
