using FluentValidation;
using Microsoft.Extensions.Logging;
using NSubstitute;
using tectest1.Api.Domain.Models;

namespace tectest1.Api.Domain.Tests
{
    public class RawMeterReadingFilepostHandlerIntegrationTests: TestBase<RawMeterReadingFilePostHandler>
    {
        private ILogger<RawMeterReadingFilePostHandler> _logger;
        private ICSVConvertor<RawMeterReading> _csvConverter;
        private IRawContentValidator _contentValidator;
        private IValidator<RawMeterReading> _validator;
        private ICreateRepository<MeterReadingUpload> _repository;

        public override void Setup()
        {

            _logger = NSubstitute.Substitute.For<ILogger<RawMeterReadingFilePostHandler>>();
            _repository = NSubstitute.Substitute.For<ICreateRepository<MeterReadingUpload>>();
            _csvConverter = new RawMeterCSVConverter();
            _contentValidator = new CustomCSVContentValidator();
            _validator = new RawMeterReadingValidator();

            _repository.Create(Arg.Any<MeterReadingUpload>()).Returns(true);
            Unit = new RawMeterReadingFilePostHandler(_logger, _csvConverter, _contentValidator, _validator, _repository, true);
        }

        [Test]
        [Category("Integration")]
        public async Task OnHandle_WithSample_ItShouldSucceedWithValidationRejections()
        {
            var text = File.ReadAllText(".\\Meter_Reading.csv");

            var actual = await Unit.Handle(new RawMeterReadingFilePostRequest() { Content = text }, new CancellationToken());

            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Reasons, Is.Not.Empty);
            Assert.That(actual.Accepted, Is.Not.EqualTo(0));
            Assert.That(actual.Rejected, Is.Not.EqualTo(0));
            Assert.Multiple(() =>
            {
                Assert.That(actual.Reasons[7].First().PropertyName, Is.EqualTo("MeterReadingValue"));
                Assert.That(actual.Reasons[7].First().ErrorMessage, Is.EqualTo("Invalid Reading."));

                Assert.That(actual.Reasons[10].First().PropertyName, Is.EqualTo("MeterReadingValue"));
                Assert.That(actual.Reasons[10].First().ErrorMessage, Is.EqualTo("Invalid Reading."));

                Assert.That(actual.Reasons[17].First().PropertyName, Is.EqualTo("MeterReadingValue"));
                Assert.That(actual.Reasons[17].First().ErrorMessage, Is.EqualTo("Invalid Reading."));

                Assert.That(actual.Reasons[18].First().PropertyName, Is.EqualTo("MeterReadingValue"));
                Assert.That(actual.Reasons[18].First().ErrorMessage, Is.EqualTo("Invalid Reading."));

                Assert.That(actual.Reasons[19].First().PropertyName, Is.EqualTo("MeterReadingValue"));
                Assert.That(actual.Reasons[19].First().ErrorMessage, Is.EqualTo("Invalid Reading."));

                Assert.That(actual.Reasons[21].First().PropertyName, Is.EqualTo("MeterReadingValue"));
                Assert.That(actual.Reasons[21].First().ErrorMessage, Is.EqualTo("'Meter Reading Value' must not be empty."));

                Assert.That(actual.Reasons[23].First().PropertyName, Is.EqualTo("MeterReadingValue"));
                Assert.That(actual.Reasons[23].First().ErrorMessage, Is.EqualTo("'Meter Reading Value' must not be empty."));

                Assert.That(actual.Reasons[26].First().PropertyName, Is.EqualTo("MeterReadingValue"));
                Assert.That(actual.Reasons[26].First().ErrorMessage, Is.EqualTo("Invalid Reading."));
            });
        }


        [Test]
        [Category("Integration")]
        public async Task OnHandle_OnRepeat_ItShouldSucceedWithValidationRejections()
        {
            _repository.Create(Arg.Any<MeterReadingUpload>()).Returns(false);
            var text = File.ReadAllText(".\\Meter_Reading.csv");

            var actual = await Unit.Handle(new RawMeterReadingFilePostRequest() { Content = text }, new CancellationToken());

            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Reasons, Has.Count.EqualTo(35));
            Assert.That(actual.Accepted, Is.EqualTo(0));
            Assert.That(actual.Rejected, Is.EqualTo(35));
           
        }


        [Test]
        [Category("Integration")]
        public async Task OnHandle_WithNoRecordsSample_ItShouldSucceedWithValidationRejections()
        {
            var text = File.ReadAllText(".\\No_Records.csv");

            var actual = await Unit.Handle(new RawMeterReadingFilePostRequest() { Content = text }, new CancellationToken());

            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Reasons, Is.Not.Empty);
            Assert.That(actual.Accepted, Is.EqualTo(0));
            Assert.That(actual.Rejected, Is.EqualTo(1));
          
        }


        [Test]
        [Category("Integration")]
        public async Task OnHandle_WithOneRecordSample_ItShouldSucceedWithNoValidationRejections()
        {
            var text = File.ReadAllText(".\\One_Record.csv");

            var actual = await Unit.Handle(new RawMeterReadingFilePostRequest() { Content = text }, new CancellationToken());

            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Reasons, Is.Empty);
            Assert.That(actual.Accepted, Is.EqualTo(1));
            Assert.That(actual.Rejected, Is.EqualTo(0));

        }
    }
}