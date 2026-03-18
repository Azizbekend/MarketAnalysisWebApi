using MarketAnalysisWebApi.DbEntities;
using MarketAnalysisWebApi.DbEntities.DbEntities;
using MarketAnalysisWebApi.DTOs.CompanyDTOs;
using MarketAnalysisWebApi.Repos.BaseRepo;

namespace MarketAnalysisWebApi.Repos.CompanyRepo
{
    public interface ICompanyRepo : IBaseRepo<DbCompany>
    {
        Task<ICollection<DbCompanyType>> GetCompanyTypes();
        Task<Guid> CreateCompany(CreateCompanyDTO dto);
        Task<DbCompany> GetByInnAsync(string inn);
    }
}
