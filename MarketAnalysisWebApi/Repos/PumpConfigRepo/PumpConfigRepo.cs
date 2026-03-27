using MarketAnalysisWebApi.DbEntities;
using MarketAnalysisWebApi.DbEntities.DbRequestConfigurations.PUMP;
using MarketAnalysisWebApi.Repos.BaseRepo;

namespace MarketAnalysisWebApi.Repos.PumpConfigRepo
{
    public class PumpConfigRepo : BaseRepo<DbPumpConfiguration>, IPumpConfigRepo
    {
        public PumpConfigRepo(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public Task<Guid> CreatePumpRequestAsync()
        {
            throw new NotImplementedException();
        }
    }
}
