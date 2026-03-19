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

        public async Task<Guid> CreateBusinesOffer(OfferCreateDTO dto)
        {
            var BO = new DbBusinessOffer
            {
                OffersNumber = dto.OfferNumber,
                NameByProject = dto.NameByProject,
                NameBySupplier = dto.NameBySupplier,
                CurrentPriceNDS = dto.CurrentPriceNDS,
                CurrentPriceNoNDS = dto.CurrentPriceNoNDS,
                SupportingDocumentDate = dto.SupportingDocumentDate.ToUniversalTime().AddHours(3),
                WarehouseLocation = dto.WarehouseLocation,
                ManufacturerCountry = dto.ManufacturerCountry,
                DeliveryTerms = dto.DeliveryTerms,
                GarantyPeriod = dto.GarantyPeriod,
                PaymentTerms = dto.PaymentTerms,
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

        public async Task<FullOfferDTO> GetFullOfferForCustomer(Guid offerId)
        {
            var BO = await _appDbContext.OffersTable.FirstOrDefaultAsync(x => x.Id == offerId);
            if (BO == null)
            {
                throw new Exception("Incorrect Id");
            }
            else
            {
                var request = await _appDbContext.ProjectRequestsTable.FirstOrDefaultAsync(x => x.Id == BO.RequestId);
                var businessAcc = await _appDbContext.BusinessAccounts.FirstOrDefaultAsync(x => x.Id == BO.BussinessAccId);
                var user = await _appDbContext.UsersTable.FirstOrDefaultAsync(x => x.Id == businessAcc.UserId);
                var copmuser = await _appDbContext.SupplierUsersCompaniesTable.FirstOrDefaultAsync(x => x.SupplierUserId == user.Id);
                var company = await _appDbContext.CompaniesTable.FirstOrDefaultAsync(x => x.Id == copmuser.CompanyId);
                if (user == null && businessAcc == null && user == null)
                {
                    throw new Exception("Incorrect Id");
                }
                else
                {
                    var fullOffer = new FullOfferDTO
                    {
                        OffersNumber = BO.OffersNumber,
                        NameByProject = BO.NameByProject,
                        NameBySupplier = BO.NameBySupplier,
                        CurrentPriceNDS = BO.CurrentPriceNDS,
                        CurrentPriceNoNDS = BO.CurrentPriceNoNDS,
                        SupportingDocumentDate = BO.SupportingDocumentDate,
                        WarehouseLocation = BO.WarehouseLocation,
                        ManufacturerCountry = BO.ManufacturerCountry,
                        SupplierSiteURL = BO.SupplierSiteURL,
                        DeliveryTerms = BO.DeliveryTerms,
                        GarantyPeriod = BO.GarantyPeriod,
                        PaymentTerms = BO.PaymentTerms,
                        FullCompanyName = company.FullCompanyName,
                        INN = company.INN,
                        KPP = company.KPP,
                        OfferFileId = BO.OfferFileId,
                        PassportFileId = BO.PassportFileId,
                        CertificateFileId = BO.CertificateFileId,
                        PlanFileId = BO.PlanFileId
                        
                    };
                    return fullOffer;
                }
            }


        }

        public async Task<Guid> UpdateOfferInfo(OfferUpdateDTO dto)
        {
            var offer = await _appDbContext.OffersTable.FirstOrDefaultAsync(x => x.Id == dto.OfferID);
            if(offer == null)
            {
                throw new Exception("not found!");
            }
            else
            {
                offer.NameByProject = dto.NameByProject;
                offer.NameBySupplier = dto.NameBySupplier;
                offer.CurrentPriceNoNDS = dto.CurrentPriceNoNDS;
                offer.CurrentPriceNDS = dto.CurrentPriceNDS;
                offer.SupportingDocumentDate = dto.SupportingDocumentDate;
                offer.WarehouseLocation = dto.WarehouseLocation;
                offer.SupplierSiteURL = dto.SupplierSiteURL;
                offer.ManufacturerCountry = dto.ManufacturerCountry;
                offer.DeliveryTerms = dto.DeliveryTerms;
                offer.GarantyPeriod = dto.GarantyPeriod;
                offer.PaymentTerms = dto.PaymentTerms;
                _appDbContext.OffersTable.Attach(offer);
                await _appDbContext.SaveChangesAsync();
                return offer.Id;
            }
        }
    }
}
