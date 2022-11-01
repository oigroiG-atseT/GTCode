using CommunityToolkit.Mvvm.ComponentModel;
using GTCode.Generators.MVVM.CommunityToolkit.Wrappers;
using Tester.ConsoleApp.Generators.Realm.Cores;

namespace Tester.ConsoleApp.Generators.Realm.Models
{
    [ObservableClassWrapper("_item")]
    public partial class ClassWrapperTestModel : ObservableObject
    {

        private readonly CoreItem _item;

        [ObservableProperty]
        private int _id;

        [ObservableProperty]
        private int _level;

        [ObservableProperty]
        private CoreInnerItem _innerItem;

        //ClassWrapperTestModel(CoreItem core) ...

        //CoreItem GetCore() ...

    }
}
