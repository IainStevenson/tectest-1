using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using tectest1.Api.Domain.Models;

namespace tectest1.Api.Domain
{
    public class RawMeterReadingFilePostHandler : IRequestHandler<RawMeterReadingFilePostRequest, RawMeterReadingFilePostResponse>
    {
        private readonly ILogger<RawMeterReadingFilePostHandler> _logger;
        private readonly ICSVConvertor<RawMeterReading> _converter;
        private readonly IValidator<RawMeterReading> _validator;
        private readonly IRawContentValidator _rawContentValidator;
        private readonly bool _fileHasHeader;
        private readonly ICreateRepository<MeterReadingUpload> _meterReadingRepository;

        public RawMeterReadingFilePostHandler(
            ILogger<RawMeterReadingFilePostHandler> logger,
            ICSVConvertor<RawMeterReading> converter,
            IRawContentValidator rawContentValidator,
            IValidator<RawMeterReading> validator,
            ICreateRepository<MeterReadingUpload> meterReadingRepository,
            bool fileHasHeader = true)
        {
            _logger = logger;
            _converter = converter;
            _validator = validator;
            _fileHasHeader = fileHasHeader;
            _rawContentValidator = rawContentValidator;
            _meterReadingRepository = meterReadingRepository;
        }

        public Task<RawMeterReadingFilePostResponse> Handle(RawMeterReadingFilePostRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling {content} characters", request.Content.Length);
            RawMeterReadingFilePostResponse response = AssumeFileIsTotallyRejected(request.Content);

            _logger.LogInformation("Validating {content} characters", request.Content.Length);
            var isValidContent = _rawContentValidator.Validate(request.Content);
            if (!isValidContent) return Task.FromResult(response);

            _logger.LogInformation("Extracting  lines");
            string[] lines = request.Content.Split("\n", StringSplitOptions.RemoveEmptyEntries);
            response.Rejected = lines.Length;

            _logger.LogInformation("Validating and cleaning lines {count}", lines.Length);
            lines = ValidateAndCleanLines(lines, response, _fileHasHeader ? 1 : 0);

            if (lines.Length == 0) return Task.FromResult(response);
            response.Rejected = lines.Length;

            _logger.LogInformation("Converting  lines {count}", lines.Length);
            Dictionary<int, RawMeterReading> items = _converter.Convert(lines, _fileHasHeader ? 2 : 1);

            _logger.LogInformation("Succcessfully converted lines {count}", items.Count(c => c.Value != null));

            StoreValidReadings(items, response);

            return Task.FromResult(response);
        }

        private void StoreValidReadings(Dictionary<int, RawMeterReading> items, RawMeterReadingFilePostResponse response)
        {
            foreach (var item in items.Where(w => w.Value != null))
            {
                var validation = _validator.Validate(item.Value);

                if (!validation.IsValid)
                {
                    response.Reasons.Add(item.Key, validation.Errors);
                    continue;
                }

                var meterReading = MapRawToObject(item.Value);

                var created = _meterReadingRepository.Create(meterReading);

                if (created)
                {
                    response.Accepted++;
                    response.Rejected--;
                }
                else
                {
                    response.Reasons.Add(item.Key, new List<ValidationFailure>() { new ValidationFailure("row", "Failed to add - account unknown or and out of date reading") });
                }
            }
        }

        private RawMeterReadingFilePostResponse AssumeFileIsTotallyRejected(string content)
        {
            return new() { Rejected = content.Length };
        }

        private MeterReadingUpload MapRawToObject(RawMeterReading item)
        {
            return new MeterReadingUpload()
            {
                AccountId = int.Parse(item.AccountId),
                MeterReadingDateTime = DateTimeOffset.Parse(item.MeterReadingDateTime),
                MeterReadingValue = int.Parse(item.MeterReadingValue)
            };
        }

        private string[] ValidateAndCleanLines(string[] lines, RawMeterReadingFilePostResponse response, int lineNo)
        {
            if (lines.Length < 2)
            {
                foreach (var line in lines)
                {
                    response.Reasons.Add(lineNo++, new List<ValidationFailure>() { new ValidationFailure("line", "invalid file") });
                }
                return new List<string>().ToArray();
            }

            return lines.Select(l => l.Replace("\r", "")).Skip(_fileHasHeader ? 1 : 0).ToArray();
        }
    }
}