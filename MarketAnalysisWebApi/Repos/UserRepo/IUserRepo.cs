using MarketAnalysisWebApi.DbEntities.DbEntities;
using MarketAnalysisWebApi.DTOs.UserDTOs;
using MarketAnalysisWebApi.Repos.BaseRepo;

namespace MarketAnalysisWebApi.Repos.UserRepo
{
    public interface IUserRepo : IBaseRepo<DbUser>
    {
        Task<Guid> CreateEmployeUser(EmployeCreateDTO dto);
    }
}
