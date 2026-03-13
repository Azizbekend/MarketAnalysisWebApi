using MarketAnalysisWebApi.DbEntities;
using MarketAnalysisWebApi.DbEntities.DbEntities;
using MarketAnalysisWebApi.DTOs.CompanyDTOs;

namespace MarketAnalysisWebApi.Repos.CompanyRepo
{
    public interface ICompanyRepo
    {
        Task<ICollection<DbCompanyType>> GetCompanyTypes();
        Task<Guid> CreateCompany(CreateCompanyDTO dto);
        Task<DbCompany> GetByInnAsync(string inn);
    }
}
