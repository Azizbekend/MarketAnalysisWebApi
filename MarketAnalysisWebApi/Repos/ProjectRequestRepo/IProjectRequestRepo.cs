using MarketAnalysisWebApi.DbEntities.DbEntities;
using MarketAnalysisWebApi.DTOs.RequestDTOs;

namespace MarketAnalysisWebApi.Repos.ProjectRequestRepo
{
    public interface IProjectRequestRepo
    {
        Task<DbProjectRequest> GetRequestByUserId(Guid requestId);
        Task CreateClickForCoins(Guid userId);
        Task<ICollection<DbProjectRequest>> GetUsersRequests(Guid userId);
        Task<ICollection<DbProjectRequest>> GetPublishedRequests();
        Task<Guid> PublishRequest(RequestStandartDTO dto);
        Task<Guid> ArchiveRequest(RequestStandartDTO dto);
    }
}
