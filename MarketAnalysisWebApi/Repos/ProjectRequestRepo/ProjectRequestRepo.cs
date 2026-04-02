using MarketAnalysisWebApi.DbEntities;
using MarketAnalysisWebApi.DbEntities.DbEntities;
using MarketAnalysisWebApi.DTOs.RequestDTOs;
using MarketAnalysisWebApi.DTOs.RequestDTOs.Supplier;
using MarketAnalysisWebApi.Repos.BaseRepo;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Ocsp;
using System.Collections.Frozen;
using System.Reflection.Metadata.Ecma335;

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

        public async Task<ICollection<JoinSupplierRequestTableDTO>> GetFavourites(Guid userId)
        {
            var requests = await _appDbContext.FavoritesTable
        .Where(fr => fr.UserId == userId)
        .Include(fr => fr.request)
            .ThenInclude(r => r.Region)
        .Include(fr => fr.request)
            .ThenInclude(r => r.RequestConfigType)
        .Include(fr => fr.request)
            .ThenInclude(r => r.Offers)
        .Include(fr => fr.request)
            .ThenInclude(r => r.KnsConfigs)
        .Include(fr => fr.request)
            .ThenInclude(r => r.PumpConfigurations)
        .ToListAsync();
                 return requests.Select(fr => new JoinSupplierRequestTableDTO
                         {
                             RequestId = fr.request.Id,
                             InnerId = fr.request.InnerId,
                             NameByProjectDocs = fr.request.NameByProjectDocs,
                             ObjectName = fr.request.ObjectName,
                             ObjectStage = fr.request.ObjectStage,
                             ProjectDocsChapter = fr.request.ProjectDocsChapter,
                             PublicationEndDate = fr.request.PublicationEndDate,
                             CustomerName = fr.request.CustomerName,
                             ProjectOrganizationName = fr.request.ProjectOrganizationName,
                             ContactName = fr.request.ContactName,
                             PhoneNumber = fr.request.PhoneNumber,
                             CreatedAt = fr.request.CreatedAt,
                             Status = fr.request.Status,
                             ViewPayStatus = null, // Заполните по необходимости
                             BusinessOffersCount = fr.request.Offers != null ? fr.request.Offers.Count : 0,
                             RegionId = fr.request.RegionId,
                             Region = fr.request.Region,
                             IsArchived = fr.request.IsArchived,
                             ConfigTypeId = fr.request.ConfigTypeId,
                             RequestConfigType = fr.request.RequestConfigType,
                             KnsConfigDTO = fr.request.KnsConfigs?.Select(k => new JoinKnsConfigDTO
                             {
                                 Efficiency = k.Perfomance,
                                 InstalationPlace = k.InstalationPlace,
                                 Untis = k.Units
                             }).FirstOrDefault(), // Заполните при наличии KnsConfigs
                             LosConfigDTO = null, // Заполните при наличии LosConfig
                             PumpConfigDTO = fr.request.PumpConfigurations?.Select(p => new JoinPumpConfigDTO
                             {
                                 Efficiency = p.PumpEfficiency,
                                 InstalationPlace = p.InstalationPlace
                             }).FirstOrDefault() // Заполните при наличии PumpConfigurations
                         })
                  .ToList();

        }

        public async Task<ICollection<JoinSupplierRequestTableDTO>> GetPublishedRequests()
        {
            var requests = await _appDbContext.ProjectRequestsTable
                .Include(r => r.Region)
                .Include(r => r.RequestConfigType)
                .Include(r => r.KnsConfigs)
                .Include(r => r.PumpConfigurations)
                .Include(r => r.AccountRequests)
                .Include(r => r.Offers)
                .Where(r => !r.IsArchived)
                .ToListAsync();
            var result = requests.Select(request => new JoinSupplierRequestTableDTO
            {
                RequestId = request.Id,
                InnerId = request.InnerId,
                NameByProjectDocs = request.NameByProjectDocs,
                ObjectName = request.ObjectName,
                ObjectStage = request.ObjectStage,
                ProjectDocsChapter = request.ProjectDocsChapter,
                PublicationEndDate = request.PublicationEndDate,
                CustomerName = request.CustomerName,
                ProjectOrganizationName = request.ProjectOrganizationName,
                ContactName = request.ContactName,
                PhoneNumber = request.PhoneNumber,
                CreatedAt = request.CreatedAt,
                Status = request.Status,
                ViewPayStatus = request.AccountRequests?.Select((vp) => vp.Status).FirstOrDefault(),
                BusinessOffersCount = request.Offers?.Count ?? 0,
                RegionId = request.RegionId,
                Region = request.Region,
                IsArchived = request.IsArchived,
                ConfigTypeId = request.ConfigTypeId,
                RequestConfigType = request.RequestConfigType,
                KnsConfigDTO = request.KnsConfigs?.Select(k => new JoinKnsConfigDTO
                {
                    Efficiency = k.Perfomance,
                    Untis = k.Units
                }).FirstOrDefault(),
                PumpConfigDTO = request.PumpConfigurations?.Select(p => new JoinPumpConfigDTO
                {
                    Efficiency = p.PumpEfficiency,

                }).FirstOrDefault()
            }).ToList();

            return result;
        }
        

        public async Task<DbProjectRequest> GetRequestById(Guid requestId)
        {
            var res = await _appDbContext.ProjectRequestsTable.FirstOrDefaultAsync(x => x.Id == requestId);
            return res;
        }
        public async Task<SupplierSingleRequestResponse> GetRequestWithStatusById(SupplierCheckRequestDTo dto)
        {
            var req = await _appDbContext.ProjectRequestsTable.FirstOrDefaultAsync(x => x.Id == dto.RequestId);
            var reg = await _appDbContext.RegionsTable.FirstOrDefaultAsync(x => x.Id == req.RegionId);
            var acc = await _appDbContext.BusinessAccounts.FirstOrDefaultAsync(acc => acc.Id == dto.AccountId);
            if(req == null || acc == null || reg == null)
            {
                throw new Exception("Current requestID or AccountID or RegionID does not exists!");
            }
            else
            {
                var accReq = await _appDbContext.AccountRequests.FirstOrDefaultAsync(key => key.AccountId == acc.Id && key.RequestId == req.Id);
                var res = new SupplierSingleRequestResponse
                {
                    InnerId = req.InnerId,
                    NameByProjectDocs = req.NameByProjectDocs,
                    ObjectName = req.ObjectName,
                    ObjectStage = req.ObjectStage,
                    ProjectDocsChapter = req.ProjectDocsChapter,
                    PublicationEndDate = req.PublicationEndDate,
                    CustomerName = req.CustomerName,
                    ProjectOrganizationName = req.ProjectOrganizationName,
                    ContactName = req.ContactName,
                    PhoneNumber = req.PhoneNumber,
                    CreatedAt = req.CreatedAt,
                    Status = req.Status,
                    IsArchived = req.IsArchived,
                    UserId = req.UserId,
                    ConfigTypeId = req.ConfigTypeId,
                    SchemeFileId = req.FileId,
                    LocationRegion = reg.RegionName
                };
                var favorite = await _appDbContext.FavoritesTable.FirstOrDefaultAsync(favor => favor.UserId == acc.UserId && favor.RequestId == dto.RequestId);
                if (favorite == null)
                {
                    res.IsFavorite = false;
                }
                else
                {
                    res.IsFavorite = true;
                }
                if (accReq != null)
                {
                    res.SupplierRequestStatus = accReq.Status;
                }
                else
                {
                    res.SupplierRequestStatus = "New";
                }
                return res;
            }
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
                            ObjectStage = req.ObjectStage,
                            ProjectDocsChapter = req.ProjectDocsChapter,
                            PublicationEndDate = req.PublicationEndDate,
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
        public async Task<ICollection<JoinSupplierRequestTableDTO>> GetFavoriteProjectRequestsByUserIdAsync(Guid userId)
        {
            var query = from request in _appDbContext.ProjectRequestsTable
                        join region in _appDbContext.RegionsTable
                            on request.RegionId equals region.Id into regionGroup
                        from region in regionGroup.DefaultIfEmpty()
                        join configType in _appDbContext.ConfigurationTypesTable
                            on request.ConfigTypeId equals configType.Id into configTypeGroup
                        from configType in configTypeGroup.DefaultIfEmpty()
                        join favorite in _appDbContext.FavoritesTable
                            on new { request.Id, UserId = userId } equals new { Id = favorite.RequestId, favorite.UserId } into favoriteGroup
                        from favorite in favoriteGroup.DefaultIfEmpty()
                        join accountRequest in _appDbContext.AccountRequests
                            on request.Id equals accountRequest.RequestId into accountGroup
                        from accountRequest in accountGroup.DefaultIfEmpty()
                        join kns in _appDbContext.KnsConfigurations
                            on request.Id equals kns.RequestId into knsGroup
                        from kns in knsGroup.DefaultIfEmpty()
                        join pump in _appDbContext.PumpConfigurations
                            on request.Id equals pump.RequestId into pumpGroup
                        from pump in pumpGroup.DefaultIfEmpty()
                        select new JoinSupplierRequestTableDTO
                        {
                            RequestId = request.Id,
                            InnerId = request.InnerId,
                            NameByProjectDocs = request.NameByProjectDocs,
                            ObjectName = request.ObjectName,
                            ObjectStage = request.ObjectStage,
                            ProjectDocsChapter = request.ProjectDocsChapter,
                            PublicationEndDate = request.PublicationEndDate,
                            CustomerName = request.CustomerName,
                            ProjectOrganizationName = request.ProjectOrganizationName,
                            ContactName = request.ContactName,
                            PhoneNumber = request.PhoneNumber,
                            CreatedAt = request.CreatedAt,
                            Status = request.Status,
                            ViewPayStatus = accountRequest != null ? accountRequest.Status : null, // Берем статус из AccountRequest
                            BusinessOffersCount = _appDbContext.OffersTable.Count(o => o.RequestId == request.Id),
                            RegionId = request.RegionId,
                            Region = region,
                            IsArchived = request.IsArchived,
                            ConfigTypeId = request.ConfigTypeId,
                            IsFavorite = favorite != null ? true : false,
                            RequestConfigType = configType,
                            KnsConfigDTO = kns != null ? new JoinKnsConfigDTO
                            {
                                Efficiency = kns.Perfomance,
                                Untis = kns.Units,
                                InstalationPlace = kns.InstalationPlace
                            } : null,
                            LosConfigDTO = null,
                            PumpConfigDTO = pump != null ? new JoinPumpConfigDTO
                            {
                                Efficiency = pump.PumpEfficiency,
                                InstalationPlace = pump.InstalationPlace
                            } : null
                        };
            var result = await query.ToListAsync();
            var distinctResult = result
                .GroupBy(r => r.RequestId)
                .Select(g => g.First())
                .ToList();
            return distinctResult;
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
                        ObjectStage = x.request.ObjectStage,
                        ProjectDocsChapter = x.request.ProjectDocsChapter,
                        PublicationEndDate = x.request.PublicationEndDate,
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
            ObjectStage = r.ObjectStage,
            ProjectDocsChapter = r.ProjectDocsChapter,
            PublicationEndDate = r.PublicationEndDate,
            ConfigTypeId = r.ConfigTypeId,
            CustomerName = r.CustomerName,
            ProjectOrganizationName = r.ProjectOrganizationName,
            ContactName = r.ContactName,
            PhoneNumber = r.PhoneNumber,
            CreatedAt = r.CreatedAt,
            Status = r.Status,
            RegionId = r.RegionId,
            Region = r.Region != null ? new DbRegion
            {
                Id = r.Region.Id,
                RegionName = r.Region.RegionName
            } : null,
            SchemeFileId = r.FileId
        })
        .FirstOrDefaultAsync();

            return result;
        }

        public async Task<ICollection<JoinSupplierRequestTableDTO>> GetUsersPublishRequests(Guid userId)
        {
            var requests = await _appDbContext.ProjectRequestsTable
                .Include(r => r.Region)
                .Include(r => r.RequestConfigType)
                .Include(r => r.KnsConfigs)
                .Include(r => r.PumpConfigurations)
                .Include(r => r.AccountRequests)
                .Include(r => r.Offers)
                .Where(r => r.UserId == userId)
                .ToListAsync();
            var result = requests.Select(request => new JoinSupplierRequestTableDTO
            {
                RequestId = request.Id,
                InnerId = request.InnerId,
                NameByProjectDocs = request.NameByProjectDocs,
                ObjectName = request.ObjectName,
                ObjectStage = request.ObjectStage,
                ProjectDocsChapter = request.ProjectDocsChapter,
                PublicationEndDate = request.PublicationEndDate,
                CustomerName = request.CustomerName,
                ProjectOrganizationName = request.ProjectOrganizationName,
                ContactName = request.ContactName,
                PhoneNumber = request.PhoneNumber,
                CreatedAt = request.CreatedAt,
                Status = request.Status,
                ViewPayStatus = request.AccountRequests?.Select((vp) => vp.Status).FirstOrDefault(),
                BusinessOffersCount = request.Offers?.Count ?? 0,
                RegionId = request.RegionId,
                Region = request.Region,
                IsArchived = request.IsArchived,
                ConfigTypeId = request.ConfigTypeId,
                RequestConfigType = request.RequestConfigType,
                KnsConfigDTO = request.KnsConfigs?.Select(k => new JoinKnsConfigDTO
                {
                    InstalationPlace = k.InstalationPlace,
                    Efficiency = k.Perfomance,
                    Untis = k.Units
                }).FirstOrDefault(),
                PumpConfigDTO = request.PumpConfigurations?.Select(p => new JoinPumpConfigDTO
                {
                    Efficiency = p.PumpEfficiency,
                    InstalationPlace = p.InstalationPlace

                }).FirstOrDefault()
            }).ToList();

            return result;
        }

        public async Task<string> CheckPayStatus(SupplierCheckRequestDTo dto)
        {
            var request = await _appDbContext.ProjectRequestsTable
                 .Include(r => r.AccountRequests)
                 .Include(r => r.Offers)
                 .Where(x => x.Id == dto.RequestId)
                 .FirstOrDefaultAsync();
            var res = request.AccountRequests.Where(x => x.AccountId == dto.AccountId).Select(x => x.Status).FirstOrDefault();
            return res;
        }

        public async Task<SupplierHalfSingleRequestResponse> GetRequestHalfRequestForSupplier(SupplierCheckRequestDTo dto)
        {
            var req = await _appDbContext.ProjectRequestsTable.FirstOrDefaultAsync(x => x.Id == dto.RequestId);
            var reg = await _appDbContext.RegionsTable.FirstOrDefaultAsync(x => x.Id == req.RegionId);
            var acc = await _appDbContext.BusinessAccounts.FirstOrDefaultAsync(acc => acc.Id == dto.AccountId);        
            if (req == null)
            {
                throw new Exception("Current requestID does not exists!");
            }
            if (acc == null)
            {
                throw new Exception("Current AccountID does not exists!");
            }
            if (reg == null)
            {
                throw new Exception("Current RegionID does not exists!");
            }
            else
            {
                var accReq = await _appDbContext.AccountRequests.FirstOrDefaultAsync(key => key.AccountId == acc.Id && key.RequestId == req.Id);
                var res = new SupplierHalfSingleRequestResponse
                {
                    InnerId = req.InnerId,
                    ObjectStage = req.ObjectStage,
                    ProjectDocsChapter = req.ProjectDocsChapter,
                    PublicationEndDate = req.PublicationEndDate,
                    CreatedAt = req.CreatedAt,
                    Status = req.Status,
                    IsArchived = req.IsArchived,
                    ConfigTypeId = req.ConfigTypeId,
                    SchemeFileId = req.FileId,
                    LocationRegion = reg.RegionName
                };
                var favorite = await _appDbContext.FavoritesTable.FirstOrDefaultAsync(favor => favor.UserId == acc.UserId && favor.RequestId == dto.RequestId);
                if (favorite == null)
                {
                    res.IsFavorite = false;
                }
                else
                {
                    res.IsFavorite = true;
                }
                if (accReq != null)
                {
                    res.SupplierRequestStatus = accReq.Status;
                }
                else
                {
                    res.SupplierRequestStatus = "New";
                }
                return res;
            }
        }

        public async Task<SupplierHalfSingleRequestResponse> GetSherryRequest(Guid id)
        {
            var result = await _appDbContext.ProjectRequestsTable
       .Include(r => r.Region)
       .Where(r => r.Id == id)
       .Select(r => new SupplierHalfSingleRequestResponse
       {
           InnerId = r.InnerId,
           ObjectStage = r.ObjectStage,
           ProjectDocsChapter = r.ProjectDocsChapter,
           PublicationEndDate = r.PublicationEndDate,
           ConfigTypeId = r.ConfigTypeId,
           CreatedAt = r.CreatedAt,
           Status = r.Status,
           Region = r.Region != null ? new DbRegion
           {
               Id = r.Region.Id,
               RegionName = r.Region.RegionName
           } : null,
           SchemeFileId = r.FileId
       })
       .FirstOrDefaultAsync();

            return result;
        }


    }
}
