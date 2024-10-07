using Business_Card.Models;
using Business_Card.Models.Dtos;
using Business_Card.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Serialization;

namespace Business_Card.Services
{
    public class BusinessCardServices : IBusinessCard
    {
        private readonly AppDbContext _context;

        public BusinessCardServices(AppDbContext context)
        {
            _context = context;
        }
        public async Task CreateBusinessCard(BusinessCard businessCard)
        {
            await _context.BusinessCards.AddAsync(businessCard);
            await _context.SaveChangesAsync();
           
        }

        public BusinessCard DeleteBusinessCard(BusinessCard businessCard)
        {
           _context.BusinessCards.Remove(businessCard);
             _context.SaveChangesAsync();
            return businessCard;

        }

        public async Task<IEnumerable<BusinessCard>> GetAllBusinessCard()
        {
          return await _context.BusinessCards.ToListAsync();
        }

        public async Task<BusinessCard> GetById(int id)
        {
            return await _context.BusinessCards.FindAsync(id);
        }



        public async Task<byte[]?> ExportBusinessCards(string format)
        {
            var businessCards = await GetAllBusinessCard();

            try
            {
                if (format.ToLower() == "csv")
                {
                    return CsvExporter.ExportToCsv(businessCards);
                }
                else if (format.ToLower() == "xml")
                {
                    return XmlExporter.ExportToXml(businessCards);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                // Handle exceptions as needed (logging, etc.)
                throw;
            }
        }



        public async Task ImportBusinessCardsFromCsv(Stream csvStream)
        {
            var businessCards = CsvHelperr.ParseCsv(csvStream);
            foreach (var dto in businessCards)
            {
                var businessCard = new BusinessCard
                {
                    BusinessCard_Name = dto.BusinessCard_Name,
                    BusinessCard_Email = dto.BusinessCard_Email,
                    BusinessCard_PhoneNumber = dto.BusinessCard_PhoneNumber,
                    BusinessCard_Address = dto.BusinessCard_Address,
                    BusinessCard_Date_Of_Birth = dto.BusinessCard_Date_Of_Birth,
                    BusinessCard_Gender = dto.BusinessCard_Gender,
                    Photo = dto.Photo
                };
                await CreateBusinessCard(businessCard);
            }
        }

        public async Task ImportBusinessCardsFromXml(Stream xmlStream)
        {
            using (StreamReader reader = new StreamReader(xmlStream))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<BusinessCard>));
                var businessCards = (List<BusinessCard>)serializer.Deserialize(reader);
                foreach (var dto in businessCards)
                {
                    //if (!IsValidBusinessCardDto(dto))
                    //{
                    //    continue;
                    //}
                    var businessCard = new BusinessCard
                    {
                        BusinessCard_Name = dto.BusinessCard_Name,
                        BusinessCard_Email = dto.BusinessCard_Email,
                        BusinessCard_PhoneNumber = dto.BusinessCard_PhoneNumber,
                        BusinessCard_Address = dto.BusinessCard_Address,
                        BusinessCard_Date_Of_Birth = dto.BusinessCard_Date_Of_Birth,
                        BusinessCard_Gender = dto.BusinessCard_Gender,
                        Photo = dto.Photo
                    };

                    await CreateBusinessCard(businessCard);
                }
            } 
        }

        private bool IsValidBusinessCardDto(BusinessCardDto dto)
        {
            // Implement validation logic as per your requirements
            return !string.IsNullOrEmpty(dto.BusinessCard_Name) &&
                   !string.IsNullOrEmpty(dto.BusinessCard_Email) &&
                   !string.IsNullOrEmpty(dto.BusinessCard_PhoneNumber);
        }






    }
}
