using System.Security.Cryptography;
using MarketAnalysisWebApi.DbEntities;
using MarketAnalysisWebApi.DbEntities.DbEntities;
using MarketAnalysisWebApi.DTOs.UserDTOs;
using MarketAnalysisWebApi.Providers;
using MarketAnalysisWebApi.Providers.EmailProvider;
using MarketAnalysisWebApi.Repos.BaseRepo;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MarketAnalysisWebApi.Repos.UserRepo
{
    public class UserRepo : BaseRepo<DbUser>, IUserRepo
    {
        private readonly IPasswordHasher<DbUser> _passwordHasher = new PasswordHasher<DbUser>();
        private readonly IMailServiceProvider _mailServiceProvider;
        public UserRepo(AppDbContext appDbContext, IMailServiceProvider provider) : base(appDbContext)
        {
            _mailServiceProvider = provider;
        }

        public async Task<Guid> CreateEmployeUser(EmployeCreateDTO dto)
        {           
            if (await _appDbContext.UsersTable.AnyAsync(x => x.Email == dto.Email)) { throw new Exception("Company not found!"); }
            else
            {
                var role = await _appDbContext.UsersRolesTable.FirstOrDefaultAsync(x => x.RoleName == dto.RoleName);
                var newEmployee = new DbUser()
                {
                    FullName = dto.FullName,
                    Email = dto.Email,
                    PhoneNumber = dto.PhoneNumber,
                    RoleId = Guid.Parse(role.Id.ToString())
                };
                var password = GenerateSecurePassword();
                newEmployee.Password = _passwordHasher.HashPassword(newEmployee, password);      
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
                var reciever = new EmailReceiver
                {
                    Name = newEmployee.FullName,
                    Address = newEmployee.Email
                };
                await _mailServiceProvider.SendPassword(reciever, password);
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

        public async Task<bool> PasswordRecoveryAsync(string email)
        {
            var user = await _appDbContext.UsersTable.FirstOrDefaultAsync(x => x.Email == email);
            if (user != null)
            {
                try
                {
                    var newPassword = GenerateSecurePassword();
                    user.Password = _passwordHasher.HashPassword(user, newPassword);
                    var EReciever = new EmailReceiver
                    {
                        Address = email,
                        Name = user.FullName
                    };
                    await _mailServiceProvider.RecoveryPassword(EReciever, newPassword);
                    _appDbContext.UsersTable.Attach(user);
                    await _appDbContext.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else
            {
                return false;
            }
        }

        public Task<Guid> SendConfirmMessageAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckConfirmCode(MailConfirmCodeDTO dto)
        {
            throw new NotImplementedException();
        }


        public string GenerateSecurePassword(int length = 12)
        {
            char[] allowedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+-=[]{}|;:,.<>?".ToCharArray();
            if (length <= 0)
                throw new ArgumentException("Длина пароля должна быть больше 0");

            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] randomBytes = new byte[length];
                rng.GetBytes(randomBytes);

                char[] password = new char[length];
                for (int i = 0; i < length; i++)
                {
                    int index = randomBytes[i] % allowedChars.Length;
                    password[i] = allowedChars[index];
                }

                return new string(password);
            }
        }

    }
}
