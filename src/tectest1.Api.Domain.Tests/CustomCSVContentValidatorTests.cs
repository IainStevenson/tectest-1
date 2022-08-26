namespace tectest1.Api.Domain.Tests
{
    public class CustomCSVContentValidatorTests : TestBase<CustomCSVContentValidator>
    {
        [Test]
        public void OnValidate_OfValidData_ItShoudlPass()
        {
            var text = File.ReadAllText(".\\Meter_Reading.csv");
            var actual = Unit.Validate(text);
            Assert.That(actual, Is.True);
        }
        [Test]
        public void OnValidate_OfEmpty_ItShoudlFail()
        {
            var actual = Unit.Validate(string.Empty);
            Assert.That(actual, Is.False);
        }

        [Test]
        public void OnValidate_OfNull_ItShoudlFail()
        {
            var actual = Unit.Validate(null);
            Assert.That(actual, Is.False);
        }


        [Test]
        public void OnValidate_OfWhiteSpace_ItShoudlFail()
        {
            var actual = Unit.Validate("      ");
            Assert.That(actual, Is.False);
        }


        [Test]
        public void OnValidate_OfBinaryData_ItShoudlFail()
        {
            var data = File.ReadAllText(".\\nunit.framework.dll");
            var actual = Unit.Validate(data);
            Assert.That(actual, Is.False);
        }

        [Test]
        public void OnValidate_OfIncorrectlyStructuredData_ItShoudlFail()
        {
            var data = File.ReadAllText(".\\Bad_Data.csv");
            var actual = Unit.Validate(data);
            Assert.That(actual, Is.False);
        }
    }
}