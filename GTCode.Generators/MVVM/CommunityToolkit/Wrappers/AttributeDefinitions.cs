namespace GTCode.Generators.MVVM.CommunityToolkit.Wrappers
{
    internal class AttributeDefinitions
    {

        public static string ObservablePropertyWrapperAttributeMetadataName()
        {
            return Globals.NAMESPACE_WRAPPERS_ATTRIBUTE + "." + ObservablePropertyWrapperAttributeName();
        }
        public static string ObservablePropertyWrapperAttributeName()
        {
            return "ObservablePropertyWrapperAttribute";
        }
        public static string ObservablePropertyWrapperAttribute()
        {
            return @"
using System;
namespace " + Globals.NAMESPACE_WRAPPERS_ATTRIBUTE + @"
{
    /// <summary>
    /// Indica che per il Field annotato da questo attributo deve essere generato un wrapper associato al nome del Field
    /// fornito con ""coreName"".
    /// Il wrapper generato avrà il nome del Field annotato in PascalNotation, rimuovendo eventuali prefissi ""_"".
    /// 
    /// E' possibile esporre classi annidate indicando il path, in dot-notation, tramite ""CorePropertyChain"".
    /// E' possibile attribuire un nome diverso da quello autogenerato specificandolo tramite ""PropertyName"". 
    /// 
    /// Esempio:
    /// <code>
    /// public partial class ItemModel : ObservableObject
    /// {
    ///     private readonly CoreItem _coreItem;
    ///     
    ///     [ObservablePropertyWrapper(""_coreItem"")]
    ///     private int _id;
    ///     [ObservablePropertyWrapper(""_coreItem"", CorePropertyChain= ""InnerCoreItem.Description"", PropertyName=""Description2"")]
    ///     private string _innerDescription;
    /// }
    /// </code>
    /// genera:
    /// <code>
    /// public partial class ItemModel : ObservableObject
    /// {
    ///     private readonly CoreItem _coreItem;
    ///
    ///     public int Id {
    ///         get => _coreItem.Id;
    ///         set => SetProperty(_coreItem.Id, value, _coreItem, (i, v) => i.Id = v);
    ///     }
    ///                 
    ///     public int Description2 {
    ///         get => _coreItem.InnerCoreItem.Description;
    ///         set => SetProperty(_coreItem.InnerCoreItem.Description, value, _coreItem, (i, v) => i.InnerCoreItem.Description = v);
    ///     }
    /// } 
    /// </summary>        
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    [System.Diagnostics.Conditional(""AutoNotifyGenerator_DEBUG"")]
    sealed class " + ObservablePropertyWrapperAttributeName() + @" : Attribute
    {
        public " + ObservablePropertyWrapperAttributeName() + @"(string coreName)
        {
            this.CoreName = coreName;
        }
        public string CoreName { get; set; }
        public string CorePropertyChain { get; set; }
        public string PropertyName { get; set; }
    }
}";
        }


        public static string ObservableValidatedPropertyWrapperAttributeMetadataName()
        {
            return Globals.NAMESPACE_WRAPPERS_ATTRIBUTE + "." + ObservableValidatedPropertyWrapperAttributeName();
        }
        public static string ObservableValidatedPropertyWrapperAttributeName()
        {
            return "ObservableValidatedPropertyWrapperAttribute";
        }
        public static string ObservableValidatedPropertyWrapperAttribute()
        {
            return @"
using System;
namespace " + Globals.NAMESPACE_WRAPPERS_ATTRIBUTE + @"
{
    /// <summary>
    /// Indica che per il Field annotato da questo attributo deve essere generato un wrapper associato al nome del Field
    /// fornito con ""coreName"".
    /// Il wrapper generato avrà il nome del Field annotato in PascalNotation, rimuovendo eventuali prefissi ""_"".
    /// 
    /// E' possibile esporre classi annidate indicando il path, in dot-notation, tramite ""CorePropertyChain"".
    /// E' possibile attribuire un nome diverso da quello autogenerato specificandolo tramite ""PropertyName"". 
    /// 
    /// Esempio:
    /// <code>
    /// public partial class ItemModel : ObservableValidator
    /// {
    ///     private readonly CoreItem _coreItem;
    ///     
    ///     [ObservableValidatedPropertyWrapper(""_coreItem"", Validators=@""
    ///         [System.ComponentModel.DataAnnotations.Required];
    ///         [System.ComponentModel.DataAnnotations.Key]
    ///     "")]
    ///     private int _id;
    ///     [ObservableValidatedPropertyWrapper(""_coreItem"", CorePropertyChain= ""InnerCoreItem.Description"", PropertyName=""Description2"", Validators=@""[System.ComponentModel.DataAnnotations.Required]"")]
    ///     private string _innerDescription;
    /// }
    /// </code>
    /// genera:
    /// <code>
    /// public partial class ItemModel : ObservableObject
    /// {
    ///     private readonly CoreItem _coreItem;
    ///
    ///     public int Id {
    ///         get => _coreItem.Id;
    ///         set => SetProperty(_coreItem.Id, value, _coreItem, (i, v) => i.Id = v, true);
    ///     }
    ///                 
    ///     public int Description2 {
    ///         get => _coreItem.InnerCoreItem.Description;
    ///         set => SetProperty(_coreItem.InnerCoreItem.Description, value, _coreItem, (i, v) => i.InnerCoreItem.Description = v, true);
    ///     }
    /// } 
    /// </summary>        
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    [System.Diagnostics.Conditional(""AutoNotifyGenerator_DEBUG"")]
    sealed class " + ObservableValidatedPropertyWrapperAttributeName() + @" : Attribute
    {
        public " + ObservableValidatedPropertyWrapperAttributeName() + @"(string coreName)
        {
            this.CoreName = coreName;            
        }
        public string CoreName { get; set; }
        public string Validators { get; set; }
        public string CorePropertyChain { get; set; }
        public string PropertyName { get; set; }
    }
}";
        }


        public static string ObservableClassWrapperAttributeName()
        {
            return "ObservableClassWrapperAttribute";
        }
        public static string ObservableClassWrapperAttribute()
        {
            return @"
using System;
namespace " + Globals.NAMESPACE_WRAPPERS_ATTRIBUTE + @"
{
    /// <summary>
    /// Indica che per la Class annotata da questo attributo deve essere generato un costruttore che esegua un binding tra il valore
    /// fornito con ""coreName"" e le Property presenti nella classe.
    ///
    /// Esempio:
    /// <code>
    /// [ObservableClassWrapper(""_coreItem"")]
    /// public partial class ItemModel : ObservableObject
    /// {
    ///     private readonly CoreItem _coreItem;
    ///     
    ///     [ObservableProperty]
    ///     private int _id;
    ///     
    ///     [ObservableProperty]
    ///     private string _description; 
    /// }
    /// </code>
    /// genera:
    /// <code>
    /// public partial class ItemModel : ObservableObject
    /// {
    ///     private readonly CoreItem _coreItem;
    ///     
    ///     public ItemModel(CoreItem core){
    ///         this._coreItem = core;
    ///         this.Id = core.Id;
    ///         this.Description = core.Description;
    ///     }
    ///     
    ///     public CoreItem GetCore(){
    ///         var ret = _coreItem;
    ///         ret.Id = this.Id;
    ///         ret.Description = this.Description;
    ///         return ret;
    ///     }
    ///     ...
    /// } 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    [System.Diagnostics.Conditional(""AutoNotifyGenerator_DEBUG"")]
    sealed class " + ObservableClassWrapperAttributeName() + @" : Attribute
    {
        public " + ObservableClassWrapperAttributeName() + @"(string coreName)
        {
            this.CoreName = coreName;            
        }
        internal string CoreName { get; set; }        
    }
}
";
        }

    }
}
