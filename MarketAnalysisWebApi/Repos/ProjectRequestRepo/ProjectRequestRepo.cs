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
                var check = await _appDbContext.AccountRequests.FirstOrDefaultAsync(x => x.AccountId == dto.AccountId && x.RequestId == dto.RequestId);
                if (check == null)
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
                else
                {
                    throw new Exception("Уже тыкался сюдой!");
                }

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
        public async Task<SupplierSingleRequestResponse> GetRequestWithStatusById(SupplierCheckRequestDTo dto)
        {
            var req = await _appDbContext.ProjectRequestsTable.FirstOrDefaultAsync(x => x.Id == dto.RequestId);
            var acc = await _appDbContext.BusinessAccounts.FirstOrDefaultAsync(acc => acc.Id == dto.AccountId);
            var accReq = await _appDbContext.AccountRequests.FirstOrDefaultAsync(key => key.AccountId == acc.Id && key.RequestId == req.Id);
            var res = new SupplierSingleRequestResponse
            {
                InnerId = req.InnerId,
                NameByProjectDocs = req.NameByProjectDocs,
                ObjectName = req.ObjectName,
                LocationRegion = req.LocationRegion,
                CustomerName = req.CustomerName,
                ProjectOrganizationName = req.ProjectOrganizationName,
                ContactName = req.ContactName,
                PhoneNumber = req.PhoneNumber,
                CreatedAt = req.CreatedAt,
                Status = req.Status,
                IsArchived = req.IsArchived,
                UserId = req.UserId,
                ConfigTypeId = req.ConfigTypeId
            };
            if(accReq != null )
            {
                res.SupplierRequestStatus = accReq.Status;
            }
            else
            {
                res.SupplierRequestStatus = "New";
            }
            return res;
        }
        public async Task<DbProjectRequest> GetRequestBySupplierId(Guid requestId)
        {
            var res = await _appDbContext.ProjectRequestsTable.FirstOrDefaultAsync(x => x.Id == requestId);
            return res;
        }

        public async Task<ICollection<FavouritesViewModel>> GetRequestWithFavouriteLinks(Guid userId)
        {
            var query = from req in _appDbContext.ProjectRequestsTable
                        join fav in _appDbContext.FavoritesTable.Where(f => f.UserId == userId)
                            on req.Id equals fav.RequestId into favorites
                        from fav in favorites.DefaultIfEmpty()
                        select new FavouritesViewModel
                        {
                            Id = req.Id,
                            InnerId = req.InnerId,
                            ObjectName = req.ObjectName,
                            LocationRegion = req.LocationRegion,
                            CustomerName = req.CustomerName,
                            CreatedAt = req.CreatedAt,
                            Status = req.Status,
                            IsArchived = req.IsArchived,
                            UserId = req.UserId,
                            ConfigTypeId = req.ConfigTypeId,
                            IsFavorite = fav != null,
                            FavoriteId = fav != null ? fav.Id : (Guid?)null
                        };

            return await query.ToListAsync();
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

        public async Task RemoveFromFavourites(RequestStandartDTO dto)
        {
            var res = await _appDbContext.FavoritesTable.FirstOrDefaultAsync(x => x.UserId == dto.UserId && x.RequestId  == dto.RequestId);
            if(res != null)
            {
                _appDbContext.FavoritesTable.Remove(res);
                await _appDbContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception("not found!");
            }
        }

        public async Task<ICollection<GetBaseRequestDTO>> GetRequestsWithRegions()
        {
            var result = await _appDbContext.ProjectRequestsTable
                .GroupJoin(
                    _appDbContext.RegionsTable,
                    request => request.RegionId,
                    region => region.Id,
                    (request, regions) => new { request, regions })
                .SelectMany(
                    x => x.regions.DefaultIfEmpty(),
                    (x, region) => new GetBaseRequestDTO
                    {
                        InnerId = x.request.InnerId,
                        NameByProjectDocs = x.request.NameByProjectDocs,
                        ObjectName = x.request.ObjectName,
                        LocationRegion = x.request.LocationRegion,
                        CustomerName = x.request.CustomerName,
                        ProjectOrganizationName = x.request.ProjectOrganizationName,
                        ContactName = x.request.ContactName,
                        PhoneNumber = x.request.PhoneNumber,
                        CreatedAt = x.request.CreatedAt,
                        Status = x.request.Status,
                        RegionId = x.request.RegionId,
                        Region = region != null ? region : null
                    }).ToListAsync();

            return result;
        }

        public async Task<GetBaseRequestDTO> GetRequestWithRegion(Guid requestId)
        {
            var result = await _appDbContext.ProjectRequestsTable
        .Include(r => r.Region)

        .Where(r => r.Id == requestId)
        .Select(r => new GetBaseRequestDTO
        {
            InnerId = r.InnerId,
            NameByProjectDocs = r.NameByProjectDocs,
            ObjectName = r.ObjectName,
            LocationRegion = r.LocationRegion,
            CustomerName = r.CustomerName,
            ProjectOrganizationName = r.ProjectOrganizationName,
            ContactName = r.ContactName,
            PhoneNumber = r.PhoneNumber,
            CreatedAt = r.CreatedAt,
            Status = r.Status,
            UserId = r.UserId,
            User = r.User != null ? new DbUser
            {
                Id = r.User.Id,
                FullName = r.User.FullName
            }:null, 
            RegionId = r.RegionId,
            Region = r.Region != null ? new DbRegion
            {
                Id = r.Region.Id,
                RegionName = r.Region.RegionName
            } : null
        })
        .FirstOrDefaultAsync();

            return result;
        }
    }
}
