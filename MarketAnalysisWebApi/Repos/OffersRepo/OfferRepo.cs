using MarketAnalysisWebApi.DbEntities;
using MarketAnalysisWebApi.DbEntities.DbEntities;
using MarketAnalysisWebApi.DTOs.OffersDTO;
using Microsoft.EntityFrameworkCore;

namespace MarketAnalysisWebApi.Repos.OffersRepo
{
    public class OfferRepo : IOfferRepo
    {
        private readonly AppDbContext _appDbContext;

        public OfferRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Guid> CreateBusinesOffet(OfferCreateDTO dto)
        {
            var BO = new DbBusinessOffer
            {
                NameByProject = dto.NameByProject,
                NameBySupplier = dto.NameBySupplier,
                CurrentPriceNDS = dto.CurrentPriceNDS,
                WarehouseLocation = dto.WarehouseLocation,
                SupplierSiteURL = dto.SupplierSiteURL,
                BussinessAccId = dto.BussinessAccId,
                RequestId = dto.RequestId
            };
            await _appDbContext.OffersTable.AddAsync(BO);
            await _appDbContext.SaveChangesAsync();
            return BO.Id;
        }

        public async Task<ICollection<DbBusinessOffer>> GetEmpoyesOffersByBusinessAccId(Guid busAccId)
        {
            var res = await _appDbContext.OffersTable.Where(x => x.BussinessAccId == busAccId).ToListAsync();
            return res;
        }

        public async Task<ICollection<DbBusinessOffer>> GetEmpoyesOffersByRequestId(Guid requestId)
        {
            var res = await _appDbContext.OffersTable.Where(x => x.RequestId == requestId).ToListAsync();
            return res;
        }

        public async Task<ICollection<DbBusinessOffer>> GetEmpoyesOffersByUser(Guid userId)
        {
            var usersBusAcc = await _appDbContext.BusinessAccounts.FirstOrDefaultAsync(x => x.UserId == userId);
            if (usersBusAcc == null)
            {
                throw new Exception("Не корректный ID пользователя");
            }
            else
            {
                var res = await _appDbContext.OffersTable.Where(x => x.BussinessAccId == usersBusAcc.Id).ToListAsync();
                return res;
            }
        }
    }
}
