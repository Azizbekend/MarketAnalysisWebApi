using MarketAnalysisWebApi.DbEntities;
using MarketAnalysisWebApi.DbEntities.DbEntities;
using MarketAnalysisWebApi.Repos.BaseRepo;

namespace MarketAnalysisWebApi.Repos.UserRepo
{
    public class UserRepo : BaseRepo<DbUser>, IUserRepo
    {
        public UserRepo(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public Task<Guid> CreateManufacturerUser()
        {
            throw new NotImplementedException();
        }

        public Task<Guid> CreateSupplierUser()
        {
            throw new NotImplementedException();
        }
    }
}
