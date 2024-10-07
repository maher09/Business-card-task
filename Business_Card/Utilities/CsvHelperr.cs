using Business_Card.Models.Dtos;
using CsvHelper;
using System.Globalization;

namespace Business_Card.Utilities
{
    public class CsvHelperr
    {
        public static List<BusinessCardDto> ParseCsv(Stream csvStream)
        {
            using (var reader = new StreamReader(csvStream))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<BusinessCardDto>().ToList();
                return records;
            }
        }
    }
}
