using System.ComponentModel;

namespace GTCode.Extensions
{
    /// <summary>
    /// Classe ideata per aggiungere nuove funzionalità a System.Enum e alle sue classi derivanti.
    /// </summary>
    public static class EnumExtension
    {

        /// <summary>
        /// Ottiene il contenuto dell'attributo Descrizione contenuto nell'enum.
        /// </summary>        
        /// <returns>Il valore contenuto in DescriptionAttribute.Description; String.Empty se privo di attributo</returns>
        public static string GetDescription(this Enum @enum)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])@enum
               .GetType()
               .GetField(@enum.ToString())
               .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }

        /// <summary>
        /// Ottiene l'attributo di un campo contenuto nell'enum.
        /// </summary>
        /// <typeparam name="T">Type dell'attributo da ottenere</typeparam>
        /// <param name="enumVal">Enum value</param>        
        /// <returns>L'attributo di type T che esiste nel valore dell'enum; Null se privo di tale attributo</returns>
        /// <example><![CDATA[string desc = MyEnum.myEnumVariable.GetAttributeOfType<DescriptionAttribute>().Description;]]></example>        
        public static T GetAttributeOfType<T>(this Enum enumVal) where T : Attribute
        {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            return (attributes.Length > 0) ? (T)attributes[0] : null;
        }

    }
}
