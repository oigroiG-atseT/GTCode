using GTCode.Utils;

namespace Tester.NUnit.GTCode.Utils
{
    [TestFixture]
    public class OptionalTests
    {

        [Category("Optionals")]
        [Order(1), Test(Description = "Verifica il funzionamento di Optional.Empty() e Optional.IsPresent()")]
        public void Test001_Empty_and_IsPresent()
        {
            var result = Optional<string>.Empty();

            Assert.That(result.IsPresent, Is.False);
        }

        [Category("Optionals")]
        [Order(2), Test(Description = "Verifica il funzionamento di Optional.Of(T) e Optional.IsPresent()")]
        public void Test002_Of_and_IsPresent()
        {
            string value = "Optional!";

            var result = Optional<string>.Of(value);

            Assert.That(result.IsPresent, Is.True);
        }

        [Category("Optionals")]
        [Order(3), Test(Description = "Verifica il funzionamento di Optional.Of(T) quando viene fornito un nullable come valore")]
        public void Test003_Of_nullValue()
        {
            string? value = null;

            Assert.Throws<ArgumentNullException>(() => Optional<string>.Of(value));            
        }

        [Category("Optionals")]
        [Order(4), Test(Description = "Verifica il funzionamento di Optional.OfNullable(T)")]
        public void Test004_OfNullable()
        {
            string? value = "Optional!";

            var result = Optional<string>.OfNullable(value);

            Assert.That(result.IsPresent, Is.True);
        }

        [Category("Optionals")]
        [Order(5), Test(Description = "Verifica il funzionamento di Optional.OfNullable(T) quando viene fornito un nullable come valore")]
        public void Test005_OfNullable_nullValue()
        {
            string? value = null;

            var result = Optional<string>.OfNullable(value);

            Assert.That(result.IsPresent, Is.False);
        }

        [Category("Optionals")]
        [Order(6), Test(Description = "Verifica il funzionamento di Optional.Get()")]
        public void Test006_Get()
        {
            string value = "Optional!";
            var optional = Optional<string>.Of(value);

            var result = optional.Get();

            Assert.That(result, Is.EqualTo(value));
        }

        [Category("Optionals")]
        [Order(7), Test(Description = "Verifica il funzionamento di Optional.Get() quando value è null")]
        public void Test007_Get_nullValue()
        {
            string? value = null;
            var optional = Optional<string>.OfNullable(value);

            Assert.Throws<InvalidOperationException>(() => optional.Get());            
        }

        [Category("Optionals")]
        [Order(8), Test(Description = "Verifica il funzionamento di Optional.IfPresent()")]
        public void Test008_IfPresent()
        {
            string value = "Optional!";
            var optional = Optional<string>.Of(value);

            string result = String.Empty;
            optional.IfPresent((val) => { result = val; });

            Assert.That(result, Is.EqualTo(value));
        }

        [Category("Optionals")]
        [Order(9), Test(Description = "Verifica il funzionamento di Optional.IfPresent() se il valore non è presente")]
        public void Test009_IfPresent_noValue()
        {            
            var optional = Optional<string>.Empty();

            string result = String.Empty;
            optional.IfPresent((val) => { result = "Edit"; });

            Assert.That(result, Is.Empty);
        }

        [Category("Optionals")]
        [Order(10), Test(Description = "Verifica il funzionamento di Optional.Filter()")]
        public void Test010_Filter()
        {
            var optional = Optional<List<string>>.Of(new List<string>() { "1", "2", "Optional!", "4"});
            
            var result = optional.Filter(value => value.Contains("Optional!"));

            Assert.That(result.IsPresent(), Is.True);
            Assert.That(result.Get().Count, Is.EqualTo(4));
            Assert.That(result.Get()[2], Is.EqualTo("Optional!"));
        }

        [Category("Optionals")]
        [Order(11), Test(Description = "Verifica il funzionamento di Optional.IfPresent() se il valore non è stato trovato")]
        public void Test011_Filter_notFound()
        {
            var optional = Optional<List<string>>.Of(new List<string>() { "1", "2", "Optional!", "4" });

            var result = optional.Filter(value => value.Contains("AAAA"));

            Assert.That(result.IsPresent(), Is.False);            
        }

        [Category("Optionals")]
        [Order(12), Test(Description = "Verifica il funzionamento di Optional.OrElse() quando value è null")]
        public void Test012_OrElse()
        {            
            var optional = Optional<string>.Empty();

            string result = optional.OrElse("Other");

            Assert.That(result, Is.EqualTo("Other"));
        }

        [Category("Optionals")]
        [Order(13), Test(Description = "Verifica il funzionamento di Optional.OrElseGet() quando value è null")]
        public void Test013_OrElseGet()
        {
            var optional = Optional<string>.Empty();

            string result = optional.OrElseGet(() => "Lambda");

            Assert.That(result, Is.EqualTo("Lambda"));
        }

        [Category("Optionals")]
        [Order(14), Test(Description = "Verifica il funzionamento di Optional.OrElseThrow() quando value è null")]
        public void Test014_OrElseThrow()
        {
            var optional = Optional<string>.Empty();

            Assert.Throws<AccessViolationException>(() => optional.OrElseThrow(() => new AccessViolationException()));            
        }

        [Category("Optionals")]
        [Order(15), Test(Description = "Verifica il funzionamento dei vari Optional.OrElse...")]
        public void Test015_OrElse_OrElseGet_OrElseThrow()
        {
            string value = "Optional!";
            var optional = Optional<string>.Of(value);

            var result1 = optional.OrElse("Fluf!");
            var result2 = optional.OrElseGet(() => "Fluf!");
            var result3 = optional.OrElseThrow(() => new AccessViolationException());

            Assert.That(result1, Is.EqualTo(value));
            Assert.That(result2, Is.EqualTo(value));
            Assert.That(result3, Is.EqualTo(value));
        }

    }
}
