using CommunityToolkit.Mvvm.ComponentModel;
using GTCode.Generators.MVVM.CommunityToolkit.Wrappers;
using System.ComponentModel.DataAnnotations;
using Tester.ConsoleApp.Generators.Realm.Cores;

namespace Tester.ConsoleApp.Generators.Realm.Models
{
    public partial class ValidatedPropertyWrapperTestModel : ObservableValidator
    {

        private readonly CoreItem _coreItem;

        public ValidatedPropertyWrapperTestModel(CoreItem coreItem) { _coreItem = coreItem; }
        
        [ObservableValidatedPropertyWrapper("_coreItem", Validators = "[System.ComponentModel.DataAnnotations.Required]")]
        private int id; //public int Id { get; set; }
        
        [ObservableValidatedPropertyWrapper("_coreItem", Validators = @"
            [System.ComponentModel.DataAnnotations.Required];
            [System.ComponentModel.DataAnnotations.MinLength(1)]
        ")]
        private string? _name; //public string Name { get; set; }
        
    }
}
