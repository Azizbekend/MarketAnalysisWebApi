using MarketAnalysisWebApi.DbEntities;
using MarketAnalysisWebApi.DbEntities.DbEntities;
using MarketAnalysisWebApi.DTOs.UserDTOs;
using MarketAnalysisWebApi.Repos.BaseRepo;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MarketAnalysisWebApi.Repos.UserRepo
{
    public class UserRepo : BaseRepo<DbUser>, IUserRepo
    {
        private readonly IPasswordHasher<DbUser> _passwordHasher = new PasswordHasher<DbUser>();
        public UserRepo(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<Guid> CreateEmployeUser(EmployeCreateDTO dto)
        {           
            if (await _appDbContext.UsersTable.AnyAsync(x => x.Email == dto.Email)) { throw new Exception("Company not found!"); }
            else
            {
                var newEmployee = new DbUser()
                {
                    FullName = dto.FullName,
                    Email = dto.Email,
                    PhoneNumber = dto.PhoneNumber,
                    RoleId = dto.RoleId
                };
                newEmployee.Password = _passwordHasher.HashPassword(newEmployee, dto.Password);
                await _appDbContext.UsersTable.AddAsync(newEmployee);
                var userCompany = new DbSupplierUserCompany
                {
                    CompanyId = dto.CompanyId,
                    SupplierUserId = newEmployee.Id
                };
                var busAcc = new DbBusinessAccount
                {
                    UserId = newEmployee.Id,
                };
                await _appDbContext.BusinessAccounts.AddAsync(busAcc);
                await _appDbContext.SupplierUsersCompaniesTable.AddAsync(userCompany);
                await _appDbContext.SaveChangesAsync();
                return newEmployee.Id;
            }
        }

        public async Task<DbBusinessAccount> GetEmployeAccountInfoAsync(Guid userId)
        {
            return await _appDbContext.BusinessAccounts.FirstOrDefaultAsync(x => x.UserId == userId);
        }
    }
}
