using Business_Card.Models.Dtos;
using System.Xml.Serialization;

namespace Business_Card.Utilities
{
    public static class XmlHelper
    {
        public static List<BusinessCardDto> ParseXml(Stream xmlStream)
        {
            // If you choose Option 2, update the XmlRootAttribute accordingly
            var serializer = new XmlSerializer(typeof(List<BusinessCardDto>), new XmlRootAttribute("ArrayOfBusinessCard"));
            using (var reader = new StreamReader(xmlStream))
            {
                try
                {
                    var businessCards = (List<BusinessCardDto>)serializer.Deserialize(reader);
                    return businessCards;
                }
                catch (InvalidOperationException ex)
                {
                    // Extract inner exception details
                    throw new Exception($"XML Deserialization Error: {ex.InnerException?.Message ?? ex.Message}");
                }
            }
        }
    }
}
