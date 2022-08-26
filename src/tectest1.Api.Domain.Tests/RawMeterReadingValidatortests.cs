namespace tectest1.Api.Domain.Tests
{
    public class RawMeterReadingValidatortests: TestBase<RawMeterReadingValidator>
    {
       

        [Test]
        public void OnValidate_WhenNoAccountId_ItShouldFail()
        {
            var actual = Unit.Validate(new Models.RawMeterReading() { MeterReadingDateTime = $"{DateTimeOffset.UtcNow}", MeterReadingValue = "123" });

            Assert.That(actual.IsValid, Is.False);
            Assert.That(actual.Errors.First().PropertyName, Is.EqualTo("AccountId"));
        }

        [Test]
        public void OnValidate_WhenNoMeterReadingDateTime_ItShouldFail()
        {
            var actual = Unit.Validate(new Models.RawMeterReading() { AccountId = $"123", MeterReadingValue = "123" });

            Assert.That(actual.IsValid, Is.False);
            Assert.That(actual.Errors.First().PropertyName, Is.EqualTo("MeterReadingDateTime"));
        }

        [Test]
        public void OnValidate_WhenNoMeterReadingValue_ItShouldFail()
        {
            var actual = Unit.Validate(new Models.RawMeterReading() { MeterReadingDateTime = $"{DateTimeOffset.UtcNow}", AccountId = "123" });


            Assert.That(actual.IsValid, Is.False);
            Assert.That(actual.Errors.First().PropertyName, Is.EqualTo("MeterReadingValue"));
        }

        [Test]
        public void OnValidate_WhenZeroMeterReadingValue_ItShouldFail()
        {
            var actual = Unit.Validate(new Models.RawMeterReading() { AccountId = "1", MeterReadingDateTime = $"{DateTimeOffset.UtcNow}", MeterReadingValue= "0" });


            Assert.That(actual.IsValid, Is.False);
            Assert.That(actual.Errors.First().PropertyName, Is.EqualTo("MeterReadingValue"));
        }

        [Test]
        public void OnValidate_WhenOutOfRangeMeterReadingValue_ItShouldFail()
        {
            var actual = Unit.Validate(new Models.RawMeterReading() { AccountId = "1", MeterReadingDateTime = $"{DateTimeOffset.UtcNow}", MeterReadingValue = "100000" });

            Assert.That(actual.IsValid, Is.False);
            Assert.That(actual.Errors.First().PropertyName, Is.EqualTo("MeterReadingValue"));
        }

        [Test]
        public void OnValidate_WhenZerAccountId_ItShouldFail()
        {
            var actual = Unit.Validate(new Models.RawMeterReading() { AccountId = "0", MeterReadingDateTime = $"{DateTimeOffset.UtcNow}", MeterReadingValue = "1" });


            Assert.That(actual.IsValid, Is.False);
            Assert.That(actual.Errors.First().PropertyName, Is.EqualTo("AccountId"));
        }
    }

}