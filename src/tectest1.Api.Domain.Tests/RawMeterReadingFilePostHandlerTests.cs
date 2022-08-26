using FluentValidation;
using Microsoft.Extensions.Logging;
using tectest1.Api.Domain.Models;

namespace tectest1.Api.Domain.Tests
{
    public class RawMeterReadingFilePostHandlerTests : TestBase<RawMeterReadingFilePostHandler>
    {
        private ILogger<RawMeterReadingFilePostHandler> _logger;
        private ICSVConvertor<RawMeterReading> _csvConverter;
        private IRawContentValidator _contentValidator;
        private IValidator<RawMeterReading> _validator;
        private ICreateRepository<MeterReadingUpload> _repository;

        public override void Setup()
        {

            _logger = NSubstitute.Substitute.For<ILogger<RawMeterReadingFilePostHandler>>();
            _csvConverter = NSubstitute.Substitute.For<ICSVConvertor<RawMeterReading>>();
            _contentValidator = NSubstitute.Substitute.For<IRawContentValidator>();
            _validator = NSubstitute.Substitute.For<IValidator<RawMeterReading>>();
            _repository = NSubstitute.Substitute.For<ICreateRepository<MeterReadingUpload>>();


            Unit = new RawMeterReadingFilePostHandler(_logger, _csvConverter, _contentValidator, _validator, _repository, true);
        }


        [Test]
        public async Task  OnHandle_ItShouldAlwaysReturnAResponse()
        {

            var actual = await Unit.Handle(new RawMeterReadingFilePostRequest(), new CancellationToken());

            Assert.That( actual, Is.Not.Null);
        }

    }
}