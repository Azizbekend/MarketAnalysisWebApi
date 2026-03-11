using MarketAnalysisWebApi.DbEntities;
using MarketAnalysisWebApi.DbEntities.DbEntities;
using MarketAnalysisWebApi.DTOs.UserDTOs;
using MarketAnalysisWebApi.Repos.BaseRepo;
using Microsoft.EntityFrameworkCore;

namespace MarketAnalysisWebApi.Repos.UserRepo
{
    public class UserRepo : BaseRepo<DbUser>, IUserRepo
    {
        public UserRepo(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<Guid> CreateEmployeUser(EmployeCreateDTO dto)
        {
            var check = await _appDbContext.CompaniesTable.FirstOrDefaultAsync(x => x.Id == dto.CompanyId);
            if (check == null) { throw new Exception("Company not found!"); }
            else
            {
                return new Guid();
            }
        }
    }
}
