using System.Diagnostics.CodeAnalysis;

namespace tectest1.Api.Domain.Models
{
    [ExcludeFromCodeCoverage]
    public partial class Account
    {
        public Account()
        {
            MeterReadingUploads = new HashSet<MeterReadingUpload>();
        }

        public int AccountId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        public virtual ICollection<MeterReadingUpload> MeterReadingUploads { get; set; }
    }
}
