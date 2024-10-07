using Business_Card.Models;
using System.Xml.Serialization;

namespace Business_Card.Utilities
{
    public class XmlImporter
    {
        public static List<BusinessCard> ImportFromXml(StreamReader reader)
        {
            var serializer = new XmlSerializer(typeof(List<BusinessCard>));
            return (List<BusinessCard>)serializer.Deserialize(reader);
        }
    }
}
