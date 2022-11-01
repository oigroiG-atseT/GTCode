using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTCode.Generators.MVVM.CommunityToolkit.Wrappers;
using Tester.ConsoleApp.Generators.Realm.Cores;

namespace Tester.ConsoleApp.Generators.Realm.Models
{
    public partial class PropertyWrapperTestModel : ObservableObject
    {

        private readonly CoreItem _coreItem;

        public PropertyWrapperTestModel(CoreItem coreItem) { _coreItem = coreItem; }

        [ObservablePropertyWrapper("_coreItem")]
        private int id; //public int Id { get; set; }

        [ObservablePropertyWrapper("_coreItem")]
        private string? _name; //public string Name { get; set; }

        [ObservablePropertyWrapper("_coreItem", PropertyName = "Item")]
        private CoreInnerItem? _innerItem; //public string Item { get; set; }

        [ObservablePropertyWrapper("_coreItem", CorePropertyChain = "InnerItem.Name")]
        private string? _innerName; //public string InnerName { get; set; }

        private string nonVisibile;

    }
}
