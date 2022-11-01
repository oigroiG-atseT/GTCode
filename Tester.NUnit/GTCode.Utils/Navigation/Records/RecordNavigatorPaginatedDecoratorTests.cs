using GTCode.Utils.Navigation.Records;

namespace Tester.NUnit.GTCode.Utils.Navigation.Records
{
    [TestFixture]
    public class RecordNavigationPaginatedDecoratorTests
    {

        private List<string> _records = new List<string>() { "A", "B" };
        private string? _seletedRecord;
        private int _currentPage;

        private RecordNavigationPaginatedDecorator<string> _navigator;

        [OneTimeSetUp]
        public void Init() 
        {
            var navigator = new RecordNavigator<string>(_records, (index) => {
                _seletedRecord = _records[index];
                return index;
            });
            _navigator = new RecordNavigationPaginatedDecorator<string>(navigator, () => {
                _currentPage++;
                _records = new List<string>() { "C", "D" };                
            }, () => {
                _currentPage--;
                _records = new List<string>() { "A", "B" };                
            }, 2, 2);
        }

        [Category("Navigation")]
        [Category("RecordPaginated")]
        [Order(1), Test(Description = "Verifica la corretta esecuzione di RecordNavigator.Start() e RecordNavigator.Next()")]
        public void Test001_Next()
        {            
            int index = 0;
            _currentPage = 1;

            _navigator.Start(_records, index);
            _navigator.Next(_currentPage);

            Assert.That(_seletedRecord, Is.EqualTo(_records[index+1]));

            //controlla che non vada OutOfBound
            for (int i = 0; i<_records.Count+10; i++) 
                _navigator.Next(_currentPage);

            Assert.That(_seletedRecord, Is.EqualTo(_records.Last()));
        }

        [Category("Navigation")]
        [Category("Record")]
        [Order(2), Test(Description = "Verifica la corretta esecuzione di RecordNavigator.Previous()")]
        public void Test002_Previous()
        {                                             
            //controlla che non vada OutOfBound
            for (int i = 0; i < _records.Count + 10; i++) 
                _navigator.Previous(_currentPage);

            Assert.That(_seletedRecord, Is.EqualTo(_records.First()));
        }

    }
}
