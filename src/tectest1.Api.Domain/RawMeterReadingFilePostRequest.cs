using MediatR;

namespace tectest1.Api.Domain
{
    public class RawMeterReadingFilePostRequest : IRequest<RawMeterReadingFilePostResponse>
    {
        public string Content { get; set; } = string.Empty;
    }
}