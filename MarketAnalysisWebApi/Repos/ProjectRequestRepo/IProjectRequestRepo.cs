using MarketAnalysisWebApi.DbEntities.DbEntities;
using MarketAnalysisWebApi.DTOs.RequestDTOs;
using MarketAnalysisWebApi.DTOs.SupplierDTOs;
using MarketAnalysisWebApi.Repos.BaseRepo;

namespace MarketAnalysisWebApi.Repos.ProjectRequestRepo
{
    public interface IProjectRequestRepo : IBaseRepo<DbProjectRequest>
    {
        Task<DbProjectRequest> GetRequestById(Guid requestId);
        Task CreateClickForCoins(SupplierCheckRequestDTo dto);
        Task<ICollection<DbProjectRequest>> GetUsersRequests(Guid userId);
        Task <Guid> CheckRequestBySupplier(SupplierCheckRequestDTo dto);
        Task<ICollection<DbProjectRequest>> GetPublishedRequests();
        Task<Guid> PublishRequest(RequestStandartDTO dto);
        Task<Guid> ArchiveRequest(RequestStandartDTO dto);
        Task<Guid> AddToFavourites(RequestStandartDTO dto);
        Task RemoveFromFavourites(RequestStandartDTO dto);
        Task<ICollection<DbProjectRequest>> GetFavourites(Guid userId);
        Task<ICollection<FavouritesViewModel>> GetRequestWithFavouriteLinks(Guid userId);
        Task<SupplierSingleRequestResponse> GetRequestWithStatusById(SupplierCheckRequestDTo dto);
        Task<ICollection<GetBaseRequestDTO>> GetRequestsWithRegions();
    }
}
