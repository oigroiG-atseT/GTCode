using GTCode.Utils.Navigation.Pagination;

namespace Tester.NUnit.GTCode.Utils.Navigation.Pagination
{
    [TestFixture]
    public class PaginationHandlerTests
    {

        private int selectionOffset;
        private int selectionLimit;

        private IPaginationHandler _handler;

        [OneTimeSetUp]
        public void Init() 
        {            
            _handler = new PaginationHandler((offset, limit) => {
                selectionOffset = offset; selectionLimit = limit;
            }, (offset, limit) => {
                selectionOffset = offset; selectionLimit = limit;
            }, 2, 4);
        }

        [Category("Navigation")]
        [Category("Pagination")]
        [Order(1), Test(Description = "Verifica la corretta esecuzione di PaginationHandler.PagineTotali()")]
        public void Test001_PagineTotali()
        {
            Assert.That(_handler.PagineTotali, Is.EqualTo(2));
        }

        [Category("Navigation")]
        [Category("Pagination")]
        [Order(2), Test(Description = "Verifica la corretta esecuzione di PaginationHandler.NextPage()")]
        public void Test002_NextPage()
        {

            int currentPage = _handler.NextPage();

            Assert.That(currentPage, Is.EqualTo(2));

            currentPage = _handler.NextPage();

            Assert.That(currentPage, Is.EqualTo(2));
            Assert.That(selectionOffset, Is.EqualTo(2));
            Assert.That(selectionLimit, Is.EqualTo(2));
        }

        [Category("Navigation")]
        [Category("Pagination")]
        [Order(3), Test(Description = "Verifica la corretta esecuzione di PaginationHandler.PreviousPage()")]
        public void Test003_PreviousPage()
        {
            int currentPage = _handler.PreviousPage();

            Assert.That(currentPage, Is.EqualTo(1));

            currentPage = _handler.PreviousPage();

            Assert.That(currentPage, Is.EqualTo(1));
            Assert.That(selectionOffset, Is.EqualTo(0));
            Assert.That(selectionLimit, Is.EqualTo(2));
        }

    }
}
