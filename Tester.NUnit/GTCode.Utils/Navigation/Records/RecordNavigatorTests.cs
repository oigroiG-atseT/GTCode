using GTCode.Utils.Navigation.Records;

namespace Tester.NUnit.GTCode.Utils.Navigation.Records
{
    [TestFixture]
    public class RecordNavigatorTests
    {

        private readonly List<string> _records = new List<string>() { "A", "B", "C", "D"};
        private string? _seletedRecord;

        private IRecordNavigator<string> _navigator;

        [OneTimeSetUp]
        public void Init() 
        {
            _navigator = new RecordNavigator<string>(_records, (index) => {
                _seletedRecord = _records[index];
                return index;
            });
        }

        [Category("Navigation")]
        [Category("Record")]
        [Order(1), Test(Description = "Verifica la corretta esecuzione di RecordNavigator.Start() e RecordNavigator.Next()")]
        public void Test001_Next()
        {
            int index = 1;

            _navigator.Start(_records, index);
            _navigator.Next();

            Assert.That(_seletedRecord, Is.EqualTo(_records[index+1]));

            //controlla che non vada OutOfBound
            for (int i = 0; i<_records.Count+10; i++) _navigator.Next();

            Assert.That(_seletedRecord, Is.EqualTo(_records.Last()));
        }

        [Category("Navigation")]
        [Category("Record")]
        [Order(2), Test(Description = "Verifica la corretta esecuzione di RecordNavigator.Previous()")]
        public void Test002_Previous()
        {
            int index = 1;

            _navigator.Start(_records, index);
            _navigator.Next();
            _navigator.Previous();

            Assert.That(_seletedRecord, Is.EqualTo(_records[index]));

            //controlla che non vada OutOfBound
            for (int i = 0; i < _records.Count + 10; i++) _navigator.Previous();

            Assert.That(_seletedRecord, Is.EqualTo(_records[index]));
        }

    }
}
