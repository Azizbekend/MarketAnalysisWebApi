using MarketAnalysisWebApi.DbEntities;
using MarketAnalysisWebApi.DbEntities.DbEntities;
using Microsoft.EntityFrameworkCore;

namespace MarketAnalysisWebApi.Repos.ProjectRequestRepo
{
    public class ProjectRequestRepo : IProjectRequestRepo
    {
        private readonly AppDbContext _context;

        public ProjectRequestRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateClickForCoins(Guid employerAccountId)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<DbProjectRequest>> GetPublishedRequests()
        {
            var res = await _context.ProjectRequestsTable.Where(x => !x.IsArchived && x.Status == RequestStatus.Published).ToListAsync();
            return res;
        }

        public async Task<DbProjectRequest> GetRequestByUserId(Guid requestId)
        {
            var res = await _context.ProjectRequestsTable.FirstOrDefaultAsync(x => x.Id == requestId);
            return res;
        }

        public async Task<ICollection<DbProjectRequest>> GetUsersRequests(Guid userId)
        {
            var res = await _context.ProjectRequestsTable.Where(x => x.UserId == userId).ToListAsync(); 
            return res;
        }
    }
}
