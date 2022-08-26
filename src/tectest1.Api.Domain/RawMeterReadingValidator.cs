using FluentValidation;
using tectest1.Api.Domain.Models;

namespace tectest1.Api.Domain
{
    public class RawMeterReadingValidator : AbstractValidator<RawMeterReading>
    {
        public RawMeterReadingValidator()
        {
            RuleFor(r => r.AccountId).NotEmpty();
            RuleFor(r => r.MeterReadingDateTime).NotEmpty();
            RuleFor(r => r.MeterReadingValue).NotEmpty();

            RuleFor(r => r.AccountId)
                .Must(x =>
                    int.TryParse(x, out var val) &&
                    val > 0)
                .WithMessage("Invalid AccountId.");

            RuleFor(r => r.MeterReadingDateTime)
                .Must(x =>
                    DateTimeOffset.TryParse(x, out var val) &&
                    val > DateTimeOffset.MinValue)
                .WithMessage("Invalid Date Time.");
            RuleFor(r => r.MeterReadingValue)
                .Must(x =>
                    int.TryParse(x, out var val) &&
                    val > 0 &&
                    val <= 99999)
                .WithMessage("Invalid Reading.");

        }
    }
}