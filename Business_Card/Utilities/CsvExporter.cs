using Business_Card.Models;
using CsvHelper;
using CsvHelper.Configuration;
using System.Formats.Asn1;
using System.Globalization;
using System.Text;

namespace Business_Card.Utilities
{
    public class CsvExporter
    {
        public static byte[] ExportToCsv(IEnumerable<BusinessCard> businessCards)
        {
            using var memoryStream = new MemoryStream();
            using var streamWriter = new StreamWriter(memoryStream, Encoding.UTF8);
            using var csvWriter = new CsvWriter(streamWriter, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
            });

            csvWriter.WriteRecords(businessCards);
            streamWriter.Flush();
            return memoryStream.ToArray();
        }
    }
}
