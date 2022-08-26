using tectest1.Api.Domain.Models;

namespace tectest1.Api.Domain
{
    public interface ICSVConvertor<T>
    {
        Dictionary<int, RawMeterReading> Convert(string[] lines, int lineOffset);
    }
}