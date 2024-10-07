using Business_Card.Models;

namespace Business_Card.Services
{
    public interface IBusinessCard
    {
        Task CreateBusinessCard(BusinessCard businessCard);
        BusinessCard DeleteBusinessCard(BusinessCard businessCard);

        Task<byte[]> ExportBusinessCards(string format);
        Task<IEnumerable<BusinessCard>> GetAllBusinessCard();
        Task<BusinessCard> GetById(int id);


        Task ImportBusinessCardsFromCsv(Stream csvStream);
        Task ImportBusinessCardsFromXml(Stream xmlStream);





    }
}
