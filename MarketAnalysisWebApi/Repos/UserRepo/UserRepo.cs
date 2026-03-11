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
            var check = await _appDbContext.CompaniesTable.FirstOrDefaultAsync(x => x.Id == dto.CompanyId);
            if (check == null) { throw new Exception("Company not found!"); }
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
                await _appDbContext.SupplierUsersCompaniesTable.AddAsync(userCompany);
                await _appDbContext.SaveChangesAsync();
                return newEmployee.Id;
            }
        }
    }
}
