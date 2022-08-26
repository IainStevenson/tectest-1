using tectest1.Api.Domain.Models;

namespace tectest1.Api.Domain
{
    public class RawMeterCSVConverter : ICSVConvertor<RawMeterReading>
    {
        private const char _columnDelimiter = ',';

        public Dictionary<int, RawMeterReading> Convert(string[] lines, int startlineNumber)
        {
            Dictionary<int, RawMeterReading> result = new();
            var lineNo = startlineNumber;
            foreach (var line in lines)
            {
                var columns = line.Split(_columnDelimiter);

                if (columns.Length < 3)
                {
                    lineNo++;
                    continue;
                }

                var newItem = new RawMeterReading()
                {
                    AccountId = columns[0],
                    MeterReadingDateTime = columns[1],
                    MeterReadingValue = columns[2],
                };
                result.Add(lineNo++, newItem);

            }
            return result;
        }
    }
}