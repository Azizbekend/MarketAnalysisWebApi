using MarketAnalysisWebApi.DbEntities;
using MarketAnalysisWebApi.DbEntities.DbEntities;
using MarketAnalysisWebApi.DbEntities.DbRequestConfigurations.PUMP;
using MarketAnalysisWebApi.DTOs.KnsCongigDTOs;
using MarketAnalysisWebApi.DTOs.PumpDTO;
using MarketAnalysisWebApi.DTOs.RequestDTOs;
using MarketAnalysisWebApi.Repos.BaseRepo;

namespace MarketAnalysisWebApi.Repos.PumpConfigRepo
{
    public class PumpConfigRepo : BaseRepo<DbPumpConfiguration>, IPumpConfigRepo
    {
        public PumpConfigRepo(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<Guid> CreateDryPump(CreateInnerDryPump dto)
        {
            var dryPump = new DbDryPump
            {
                RequestId = dto.RequestId,
                PumpTypeId = dto.PumpTypeId,
                InstalationType = dto.InstalationType,
                SuctionHeight = dto.SuctionHeight
            };
            await _appDbContext.DryPumps.AddAsync(dryPump);
            await _appDbContext.SaveChangesAsync();
            return dto.RequestId;
        }

        public async Task<Guid> CreateSubPump(CreateInnerSubPump dto)
        {
            var subPump = new DbSubmersiblePump
            {
                RequestId = dto.RequestId,
                PumpTypeId = dto.PumpTypeId,
                InstalationType = dto.InstalationType,
                PotentialDepth = dto.PotentialDepth
            };
            await _appDbContext.SubmersiblePumps.AddAsync(subPump);
            await _appDbContext.SaveChangesAsync();
            return subPump.RequestId;
        }

        public async Task<Guid> CreatePumpConfig(CreateInnerPumpConfig dto)
        {
            var pumpConfig = new DbPumpConfiguration
            {
                RequestId = dto.RequestId,
                PumpTypeId = dto.PumpTypeId,
                PumpedLiquidType = dto.PumpedLiquidType,
                PumpEfficiency = dto.PumpEfficiency,
                PumpDiameter = dto.PumpDiameter,
                LiquidTemperature = dto.LiquidTemperature,
                MineralParticlesSize = dto.MineralParticlesSize,
                MineralParticlesConcentration = dto.MineralParticlesConcentration,
                BigParticleExistance = dto.BigParticleExistance,
                SpecificWastes = dto.SpecificWastes,
                LiquidDensity = dto.LiquidDensity,
                RequiredPressure = dto.RequiredPressure,
                RequiredOutPressure = dto.RequiredOutPressure,
                PressureLoses = dto.PressureLoses,
                NetworkLength = dto.NetworkLength,
                PipesConditions = dto.PipesConditions,
                GeodesicalMarks = dto.GeodesicalMarks,
                ExplosionProtection = dto.ExplosionProtection,
                ControlType = dto.ControlType,
                PowerCableLength = dto.PowerCableLength,
                WorkPower = dto.WorkPower,
                FrequencyConverter = dto.FrequencyConverter,
                PowerCurrentType = dto.PowerCurrentType,
                LiftingTransportEquipment = dto.LiftingTransportEquipment,
                FlushValve = dto.FlushValve,
                OtherLevelMeters = dto.OtherLevelMeters,
                OtherRequirements = dto.OtherRequirements, 
            };
            await _appDbContext.PumpConfigurations.AddAsync(pumpConfig);
            await _appDbContext.SaveChangesAsync();
            return pumpConfig.Id;
        }


        public async Task<Guid> CreatePumpRequestAsync(CreateInnerRequestDTO dto)
        {
            var pumpRequest = new DbProjectRequest
            {
                NameByProjectDocs = dto.NameByProjectDocs,
                ObjectName = dto.ObjectName,
                LocationRegion = dto.LocationRegion,
                CustomerName = dto.CustomerName,
                ProjectOrganizationName = dto.ProjectOrganizationName,
                ContactName = dto.ContactName,
                PhoneNumber = dto.PhoneNumber,
                ConfigTypeId = dto.ConfigTypeId,
                UserId = dto.UserId,
                Status = RequestStatus.New,
                InnerId = await GenerateRequestNumber(dto.UserId)
            };
            await _appDbContext.ProjectRequestsTable.AddAsync(pumpRequest);
            await _appDbContext.SaveChangesAsync();
            return pumpRequest.Id;
        }


    }
}
