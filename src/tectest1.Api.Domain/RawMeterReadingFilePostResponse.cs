using FluentValidation.Results;
using System.Text.Json.Serialization;

namespace tectest1.Api.Domain
{
    public class RawMeterReadingFilePostResponse
    {
        public int Accepted { get; set; }
        [JsonIgnore]
        public Dictionary<int, List<ValidationFailure>> Reasons { get; set; } = new();
        public int Rejected
        {
            get; set;
        }
    }
}