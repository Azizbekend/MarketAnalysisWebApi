using MarketAnalysisWebApi.DbEntities;
using MarketAnalysisWebApi.DbEntities.DbEntities;
using MarketAnalysisWebApi.DTOs.RequestDTOs;
using Microsoft.EntityFrameworkCore;

namespace MarketAnalysisWebApi.Repos.InnerHelperRepo
{
    public class InnerHelperRepo : IInnerHelperRepo
    {
        private readonly AppDbContext _appDbContext;
        

        public InnerHelperRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Guid> AttachRegion(Guid requestId, Guid regionId)
        {
            var request = await _appDbContext.ProjectRequestsTable.FirstOrDefaultAsync(x => x.Id == requestId);
            request?.RegionId = regionId;
            _appDbContext.ProjectRequestsTable.Attach(request);
            await _appDbContext.SaveChangesAsync();
            return requestId;
        }

        public async Task CreateKnsEquipment(string name)
        {
            var equipment = new DbEquipment
            {
                Name = name,
                ConfigTypeId = Guid.Parse("019cdcd9-1892-7f3a-955c-3503ede15a6d")
            };
            _appDbContext.EquipmentTable.Add(equipment);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task CreateNewRequestConfig(string name)
        {
            var config = new DbRequestConfigType { ConfigTypeName = name };
            _appDbContext.ConfigurationTypesTable.Add(config);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<Guid> CreateRegion(string name)
        {
            var newRegion = new DbRegion
            {
                RegionName = name
            };
            await _appDbContext.RegionsTable.AddAsync(newRegion);
            await _appDbContext.SaveChangesAsync();
            return newRegion.Id;
        }

        public async Task<ICollection<DbRegion>> GetAllRegions()
        {
            var res = await _appDbContext.RegionsTable.ToListAsync();
            return res;
        }

        public async Task InnerCreateBusinessAcc(Guid userId)
        {
            var BA = new DbBusinessAccount { UserId = userId };
            await _appDbContext.AddAsync(BA);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task InnerCreateCompanyType(string name)
        {
            var type = new DbCompanyType
            {
                TypeName = name
            };
            await _appDbContext.CompanyTypesTable.AddAsync(type);
            await _appDbContext.SaveChangesAsync();

        }

        public async Task<Guid> RequestArchive(Guid requestId)
        {
            var request = await _appDbContext.ProjectRequestsTable.FirstOrDefaultAsync(x => x.Id == requestId);
            if (request == null)
            {
                throw new Exception("Request not found!");
            }
            else
            {
                request.IsArchived = !request.IsArchived;
                _appDbContext.ProjectRequestsTable.Attach(request);
                await _appDbContext.SaveChangesAsync();
                return request.Id;
            }
        }

        public async Task<Guid> RequestStatusChangeAsync(RequestStasusChangeDTo dto)
        {
            var request = await _appDbContext.ProjectRequestsTable.FirstOrDefaultAsync(x => x.Id == dto.RequestId);
            if(request == null)
            {
                throw new Exception("Request not found!");
            }
            else
            {
                request.Status = dto.NewStatus;
                _appDbContext.ProjectRequestsTable.Attach(request);
                await _appDbContext.SaveChangesAsync();
                return request.Id;
            }
        }
    }
}
