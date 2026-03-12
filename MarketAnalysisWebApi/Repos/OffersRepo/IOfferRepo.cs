using MarketAnalysisWebApi.DbEntities.DbEntities;
using MarketAnalysisWebApi.DTOs.OffersDTO;

namespace MarketAnalysisWebApi.Repos.OffersRepo
{
    public interface IOfferRepo
    {
        Task<Guid> CreateBusinesOffer(OfferCreateDTO dto);
        Task<ICollection<DbBusinessOffer>> GetEmpoyesOffersByUser(Guid userId);
        Task<ICollection<DbBusinessOffer>> GetEmpoyesOffersByRequestId(Guid requestId);
        Task<ICollection<DbBusinessOffer>> GetEmpoyesOffersByBusinessAccId(Guid busAccId);
        Task<FullOfferDTO> GetFullOfferForCustomer(Guid offerId);
    }
}
