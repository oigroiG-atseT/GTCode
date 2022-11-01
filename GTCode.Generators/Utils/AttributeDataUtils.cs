using Microsoft.CodeAnalysis;
using System.Collections.Immutable;

namespace GTCode.Generators.Utils
{
    internal class AttributeDataUtils
    {

        /// <summary>
        /// Estrapola il parametro fornito al costruttore nella posizione indicata da 'position'.
        /// </summary>
        /// <param name="attributeData">AttributeData dalla quale estrapolare il valore fornito nel costruttore.
        /// AttributeData viene estrapolato tramite:
        /// <code>fieldSymbol.GetAttributes().Single(ad => ad.AttributeClass.Equals(attributeSymbol, SymbolEqualityComparer.Default));</code>
        /// </param>
        /// <param name="position">Posizione del parametro del costruttore da ottenre. Parte da 0.</param>
        /// <returns>Il valore fornito al costruttore</returns>
        public static string GetConstructorArgumentValue(AttributeData attributeData, int position)
        {
            ImmutableArray<TypedConstant> args = attributeData.ConstructorArguments;

            foreach (TypedConstant arg in args)
            {
                if (arg.Kind == TypedConstantKind.Error)
                {
                    //TODO: decidere come gestire l'errore, sollevo diagnostic?
                    return null;
                }
            }

            //TODO: decidere come gestire l'errore, sollevo diagnostic?
            if (args.Length < position) return null;

            return (string)args[0].Value; ;
        }
        
        /// <summary>
        /// Ottiene il valore contenuto nella property con nome uguale a quello fornito con 'propertyName'.
        /// Se non è valorizzata ritorna String.Empty.
        /// </summary>
        /// <param name="attributeData">AttributeData dalla quale estrapolare il valore fornito nel costruttore.
        /// AttributeData viene estrapolato tramite:
        /// <code>fieldSymbol.GetAttributes().Single(ad => ad.AttributeClass.Equals(attributeSymbol, SymbolEqualityComparer.Default));</code>
        /// </param>
        /// <param name="propertyName">Nome della property della quale ottenerne il valore.</param>
        /// <returns>Il valore contenuto nella property.</returns>
        public static string GetPropertyValue(AttributeData attributeData, string propertyName)
        {
            TypedConstant coreName = attributeData.NamedArguments.SingleOrDefault(kvp => kvp.Key == propertyName).Value;
            return (coreName.Value is null) ? String.Empty : coreName.Value.ToString();
        }

    }
}
