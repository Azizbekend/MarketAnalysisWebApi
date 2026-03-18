using MarketAnalysisWebApi.DbEntities;
using MarketAnalysisWebApi.DbEntities.DbEntities;
using MarketAnalysisWebApi.DTOs.RequestDTOs;
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

        public async Task<Guid> ArchiveRequest(RequestStandartDTO dto)
        {
            var request = await _context.ProjectRequestsTable.FirstOrDefaultAsync(x => x.UserId == dto.UserId && x.Id == dto.RequestId);
            if(request != null && !request.IsArchived)
            {
                request.IsArchived = true;
                _context.ProjectRequestsTable.Attach(request);
                await _context.SaveChangesAsync();
                return request.Id;
            }
            else
            {
                throw new Exception("Данной заявки не существует или у Вас нет прав для работы с ней");
            }
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

        public async Task<Guid> PublishRequest(RequestStandartDTO dto)
        {
            var request = await _context.ProjectRequestsTable.FirstOrDefaultAsync(x => x.UserId == dto.UserId && x.Id == dto.RequestId);
            if(request != null && !request.IsArchived)
            {
                request.Status = RequestStatus.Published;
                _context.ProjectRequestsTable.Attach(request);
                await _context.SaveChangesAsync();
                return request.Id;
            }
            else
            {
                throw new Exception("Данной заявки не существует или у Вас нет прав для работы с ней");
            }
        }
    }
}
