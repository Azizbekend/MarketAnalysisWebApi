using MarketAnalysisWebApi.DbEntities.DbEntities;
using MarketAnalysisWebApi.DTOs.UserDTOs;
using MarketAnalysisWebApi.Repos.BaseRepo;

namespace MarketAnalysisWebApi.Repos.UserRepo
{
    public interface IUserRepo : IBaseRepo<DbUser>
    {
        Task<Guid> CreateEmployeUser(EmployeCreateDTO dto);
        Task<DbBusinessAccount> GetEmployeAccountInfoAsync(Guid userId);
        Task<bool> PasswordRecoveryAsync(string email);
        Task<Guid> SendConfirmMessageAsync(Guid userId);
        Task<bool> CheckConfirmCode(MailConfirmCodeDTO dto);
        Task<bool> CheckEmailExistance(string email);
        Task DeleteCascade(Guid userId) ;
    }
}
