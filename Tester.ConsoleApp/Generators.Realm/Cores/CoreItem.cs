namespace Tester.ConsoleApp.Generators.Realm.Cores
{
    public class CoreItem
    {

        public int Id { get; set; }

        public string Name { get; set; } = String.Empty;

        public int Level { get; set; }

        public CoreInnerItem? InnerItem { get; set; }

    }
}
