using MarketAnalysisWebApi.DbEntities;
using MarketAnalysisWebApi.DbEntities.DbEntities;
using MarketAnalysisWebApi.DTOs.RequestDTOs;
using MarketAnalysisWebApi.DTOs.SupplierDTOs;
using MarketAnalysisWebApi.Repos.BaseRepo;
using Microsoft.EntityFrameworkCore;

namespace MarketAnalysisWebApi.Repos.ProjectRequestRepo
{
    public class ProjectRequestRepo : BaseRepo<DbProjectRequest>,  IProjectRequestRepo
    {
        private const int price = 1;
        public ProjectRequestRepo(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<Guid> AddToFavourites(RequestStandartDTO dto)
        {
            var favourite = new DbFavoriteRequest
            {
                RequestId = dto.RequestId,
                UserId = dto.UserId,
            };
            await _appDbContext.FavoritesTable.AddAsync(favourite);
            await _appDbContext.SaveChangesAsync();
            return favourite.Id;        
        }

        public async Task<Guid> ArchiveRequest(RequestStandartDTO dto)
        {
            var request = await _appDbContext.ProjectRequestsTable.FirstOrDefaultAsync(x => x.UserId == dto.UserId && x.Id == dto.RequestId);
            if(request != null && !request.IsArchived)
            {
                request.IsArchived = true;
                _appDbContext.ProjectRequestsTable.Attach(request);
                await _appDbContext.SaveChangesAsync();
                return request.Id;
            }
            else
            {
                throw new Exception("Данной заявки не существует или у Вас нет прав для работы с ней");
            }
        }

        public async Task<Guid> CheckRequestBySupplier(SupplierCheckRequestDTo dto)
        {
            var request = await _appDbContext.ProjectRequestsTable.FirstOrDefaultAsync(x => x.Id == dto.RequestId);

            if (request == null)
            {
                throw new Exception("Неверный ввод данных");
            }
            else
            {
                
                var requestLink = new DbAccountRequest
                {
                    AccountId = dto.AccountId,
                    RequestId = request.Id,
                    Status = "Viewed"
                };
                await _appDbContext.AccountRequests.AddAsync(requestLink);
                await _appDbContext.SaveChangesAsync();
                return requestLink.Id;
            }
        }


        public async Task CreateClickForCoins(SupplierCheckRequestDTo dto)
        {
            var account = await _appDbContext.BusinessAccounts.FirstOrDefaultAsync(x => x.Id == dto.AccountId);
            if(account == null || account.Coins <= 0)
            {
                throw new Exception("Не хватает монет");
            }
            else
            {
                var link = await _appDbContext.AccountRequests.FirstOrDefaultAsync(x => x.AccountId == dto.AccountId && x.RequestId == dto.RequestId);
                link.Status = "Payed";
                account.Coins -= price;
                _appDbContext.AccountRequests.Attach(link);
                _appDbContext.BusinessAccounts.Attach(account);
                await _appDbContext.SaveChangesAsync();

            }
        }

        public async Task<ICollection<DbProjectRequest>> GetFavourites(Guid userId)
        {
            var list = new List<DbProjectRequest>();
            var link = await _appDbContext.FavoritesTable.Where(x => x.UserId == userId).ToListAsync();
            foreach(var favourite in link)
            {
                var buff = await _appDbContext.ProjectRequestsTable.FirstOrDefaultAsync(x => x.Id == favourite.RequestId);
                list.Add(buff);
            }
            return list;

        }

        public async Task<ICollection<DbProjectRequest>> GetPublishedRequests()
        {
            var res = await _appDbContext.ProjectRequestsTable.Where(x => !x.IsArchived && x.Status == RequestStatus.Published).ToListAsync();
            return res;
        }

        public async Task<DbProjectRequest> GetRequestById(Guid requestId)
        {
            var res = await _appDbContext.ProjectRequestsTable.FirstOrDefaultAsync(x => x.Id == requestId);
            return res;
        }
        public async Task<DbProjectRequest> GetRequestBySupplierId(Guid requestId)
        {
            var res = await _appDbContext.ProjectRequestsTable.FirstOrDefaultAsync(x => x.Id == requestId);
            return res;
        }
        public async Task<ICollection<DbProjectRequest>> GetUsersRequests(Guid userId)
        {
            var res = await _appDbContext.ProjectRequestsTable.Where(x => x.UserId == userId).ToListAsync(); 
            return res;
        }

        public async Task<Guid> PublishRequest(RequestStandartDTO dto)
        {
            var request = await _appDbContext.ProjectRequestsTable.FirstOrDefaultAsync(x => x.UserId == dto.UserId && x.Id == dto.RequestId);
            if(request != null && !request.IsArchived)
            {
                request.Status = RequestStatus.Published;
                _appDbContext.ProjectRequestsTable.Attach(request);
                await _appDbContext.SaveChangesAsync();
                return request.Id;
            }
            else
            {
                throw new Exception("Данной заявки не существует или у Вас нет прав для работы с ней");
            }
        }
    }
}
