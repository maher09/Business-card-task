using Business_Card.Models;
using System.Text;
using System.Xml.Serialization;

namespace Business_Card.Utilities
{
    public class XmlExporter
    {
        public static byte[] ExportToXml(IEnumerable<BusinessCard> businessCards)
        {
            var serializer = new XmlSerializer(typeof(List<BusinessCard>));
            using var memoryStream = new MemoryStream();
            using var writer = new StreamWriter(memoryStream, Encoding.UTF8);
            serializer.Serialize(writer, new List<BusinessCard>(businessCards));
            writer.Flush();
            return memoryStream.ToArray();
        }
    }
}
