namespace tectest1.Api.Domain.Tests
{
    public class RawMeterCSVConverterTests: TestBase<RawMeterCSVConverter>
    {
        private string[] _lines = Array.Empty<string>();

        public override void Setup()
        {
            base.Setup();

            var text = File.ReadAllText(".\\Meter_Reading.csv");
            _lines = text.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);


        }
        [Test]
        public void OnConvert_OfSampleFile_ItShouldReturnTheNumberOfLines()
        {

            var actual = Unit.Convert(_lines, 1);

            Assert.That(actual, Is.Not.Empty);
            Assert.That(actual.Count, Is.EqualTo(_lines.Count()));

        }
        [Test]
        public void OnConvert_OfEmptySample_ItShouldReturnTheNumberOfLines()
        {
            _lines = new string[] { "AccountId,MeterReadingDateTime,MeterReadValue," };
            var actual = Unit.Convert(_lines, 1);

            Assert.That(actual, Is.Not.Empty);
            Assert.That(actual.Count, Is.EqualTo(_lines.Count()));

        }

        [Test]
        public void OnConvert_OfTooFewColumnsSample_ItShouldReturnEmptyLines()
        {
            var text = File.ReadAllText(".\\Bad_Columns.csv");
            _lines = text.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            var actual = Unit.Convert(_lines, 1);

            Assert.That(actual, Is.Empty);

        }
        [Test]
        public void OnConvert_OfNothing_ItShouldReturnAnEmptyArray()
        {

            var actual = Unit.Convert(Array.Empty<string>(), 1);

            Assert.That(actual, Is.Empty);


        }

    }
}