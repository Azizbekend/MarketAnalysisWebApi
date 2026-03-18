using MarketAnalysisWebApi.DbEntities.DbEntities;
using MarketAnalysisWebApi.DTOs.RequestDTOs;
using MarketAnalysisWebApi.Repos.BaseRepo;

namespace MarketAnalysisWebApi.Repos.ProjectRequestRepo
{
    public interface IProjectRequestRepo : IBaseRepo<DbProjectRequest>
    {
        Task<DbProjectRequest> GetRequestById(Guid requestId);
        Task CreateClickForCoins(Guid userId);
        Task<ICollection<DbProjectRequest>> GetUsersRequests(Guid userId);
        Task<ICollection<DbProjectRequest>> GetPublishedRequests();
        Task<Guid> PublishRequest(RequestStandartDTO dto);
        Task<Guid> ArchiveRequest(RequestStandartDTO dto);
    }
}
