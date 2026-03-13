using MarketAnalysisWebApi.DbEntities;
using MarketAnalysisWebApi.DbEntities.DbEntities;
using MarketAnalysisWebApi.DTOs.CompanyDTOs;
using Microsoft.EntityFrameworkCore;

namespace MarketAnalysisWebApi.Repos.CompanyRepo
{
    public class CompanyRepo : ICompanyRepo
    {
        private readonly AppDbContext _appDbContext;

        public CompanyRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Guid> CreateCompany(CreateCompanyDTO dto)
        {
            var check = await _appDbContext.CompaniesTable.FirstOrDefaultAsync(x => x.INN == dto.INN);
            if (check == null)
            {
                var newCompany = new DbCompany
                {
                    FullCompanyName = dto.FullCompanyName,
                    ShortCompanyName = dto.ShortCompanyName,
                    INN = dto.INN,
                    KPP = dto.KPP,
                    JurAdress = dto.JurAdress,
                    CompanyTypeId = dto.CompanyTypeId,
                };
                await _appDbContext.CompaniesTable.AddAsync(newCompany);
                await _appDbContext.SaveChangesAsync();
                return newCompany.Id;
            }
            else
            {
                throw new Exception("This company already exists!");
            }

        }

        public async Task<DbCompany> GetByInnAsync(string inn)
        {
            return await _appDbContext.CompaniesTable.FirstOrDefaultAsync(x => x.INN == inn);
        }

        public async Task<ICollection<DbCompanyType>> GetCompanyTypes()
        {
            return await _appDbContext.CompanyTypesTable.ToListAsync();
        }
    }
}
