using GTCode.Extensions;

namespace Tester.NUnit.GTCode.Extensions
{
    [TestFixture]
    public class EnumExtensionTests
    {

        private enum TestEnum
        {
            [System.ComponentModel.Description("PRIMO")]
            TEST1 = 1,
            [System.ComponentModel.Description("SECONDO")]
            TEST2 = 2,
            TEST3 = 3
        }

        [Category("Enum")]
        [Category("Extension")]
        [Order(0), Test(Description = "Verifica la corretta esecuzione di Enum.GetDescription()")]
        public void Test000_GetDescription()
        {
            string expected = "PRIMO";
            
            var result = TestEnum.TEST1.GetDescription();

            Assert.That(result, Is.EqualTo(expected));
        }

        [Category("Enum")]
        [Category("Extension")]
        [Order(1), Test(Description = "Verifica la corretta esecuzione di Enum.GetDescription() nel caso il campo non abbia attributi")]
        public void Test001_GetDescription_noAttribute()
        {
            string expected = String.Empty;

            var result = TestEnum.TEST3.GetDescription();

            Assert.That(result, Is.EqualTo(expected));
        }

        [Category("Enum")]
        [Category("Extension")]
        [Order(2), Test(Description = "Verifica la corretta esecuzione di Enum.GetAttributeOfType<T>()")]
        public void Test002_GetAttributeOfType()
        {
            string expected = "SECONDO";

            var result = TestEnum.TEST2.GetAttributeOfType<System.ComponentModel.DescriptionAttribute>().Description;

            Assert.That(result, Is.EqualTo(expected));
        }

        [Category("Enum")]
        [Category("Extension")]
        [Order(2), Test(Description = "Verifica la corretta esecuzione di Enum.GetAttributeOfType<T>() nel caso il campo non abbia attributi")]
        public void Test003_GetAttributeOfType_noAttribute()
        {
            var result = TestEnum.TEST3.GetAttributeOfType<System.ComponentModel.DescriptionAttribute>();

            Assert.That(result, Is.Null);
        }

    }
}
