using System.Diagnostics.CodeAnalysis;

namespace tectest1.Api.Domain.Models
{
    [ExcludeFromCodeCoverage]
    public class RawMeterReading
    {
        public string AccountId { get; set; } = string.Empty;
        public string MeterReadingDateTime { get; set; } = string.Empty;
        public string MeterReadingValue { get; set; } = string.Empty;
    }
}