using MarketAnalysisWebApi.DbEntities.DbRequestConfigurations.PUMP;
using MarketAnalysisWebApi.DTOs.PumpDTO;
using MarketAnalysisWebApi.DTOs.RequestDTOs;
using MarketAnalysisWebApi.Repos.BaseRepo;

namespace MarketAnalysisWebApi.Repos.PumpConfigRepo
{
    public interface IPumpConfigRepo : IBaseRepo<DbPumpConfiguration>
    {
        Task<Guid> CreatePumpRequestAsync(CreateInnerRequestDTO dto);
        Task<Guid> CreatePumpConfig(CreateInnerPumpConfig dto);
        Task<Guid> CreateDryPump(CreateInnerDryPump dto);
        Task<Guid> CreateSubPump(CreateInnerSubPump dto);
        Task<ICollection<DbPumpType>> GetTypes();
       
    }
}
