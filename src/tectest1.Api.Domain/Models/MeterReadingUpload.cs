using System.Diagnostics.CodeAnalysis;

namespace tectest1.Api.Domain.Models
{
    [ExcludeFromCodeCoverage]
    public partial class MeterReadingUpload
    {
        public int MeterReadingUploadsId { get; set; }
        public int AccountId { get; set; }
        public DateTimeOffset MeterReadingDateTime { get; set; }
        public int MeterReadingValue { get; set; }

        public virtual Account Account { get; set; } = null!;
    }
}
