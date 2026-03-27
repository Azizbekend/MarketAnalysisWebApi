using MarketAnalysisWebApi.DbEntities.DbRequestConfigurations.PUMP;
using MarketAnalysisWebApi.Repos.BaseRepo;

namespace MarketAnalysisWebApi.Repos.PumpConfigRepo
{
    public interface IPumpConfigRepo : IBaseRepo<DbPumpConfiguration>
    {
        Task<Guid> CreatePumpRequestAsync();
       
    }
}
