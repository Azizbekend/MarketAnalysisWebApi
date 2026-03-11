using MarketAnalysisWebApi.DbEntities.DbEntities;

namespace MarketAnalysisWebApi.Repos.ProjectRequestRepo
{
    public interface IProjectRequestRepo
    {
        Task<DbProjectRequest> GetRequestByUserId(Guid requestId);
        Task<ICollection<DbProjectRequest>> GetUsersRequests(Guid userId);
    }
}
