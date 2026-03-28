using MarketAnalysisWebApi.DbEntities;
using MarketAnalysisWebApi.DbEntities.DbEntities;
using MarketAnalysisWebApi.DbEntities.DbRequestConfigurations.PUMP;
using MarketAnalysisWebApi.DTOs.KnsCongigDTOs;
using MarketAnalysisWebApi.DTOs.PumpDTO;
using MarketAnalysisWebApi.DTOs.RequestDTOs;
using MarketAnalysisWebApi.Repos.BaseRepo;
using Microsoft.EntityFrameworkCore;

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
                PumpTypeId = dto.PumpTypeId,
                InstalationType = dto.InstalationType,
                PotentialDepth = dto.PotentialDepth
            };
            await _appDbContext.SubmersiblePumps.AddAsync(subPump);
            await _appDbContext.SaveChangesAsync();
            return subPump.Id;
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

        public async Task<ICollection<DbPumpType>> GetTypes()
        {
            var list = await _appDbContext.PumpTypes.ToListAsync();
            return list;
        }

        public async Task<JoinPumpConfigDTO> GetPumpConfiguration(Guid requestId)
        {
            var res = await _appDbContext.PumpConfigurations
                .Include(t => t.Type)
                    .ThenInclude(d => d.DryPumps)
                .Include(t => t.Type)
                    .ThenInclude(s => s.SubmersiblePumps)
                    .Select( c => new JoinPumpConfigDTO
                    {
                        PumpEfficiency = c.PumpEfficiency,
                        PumpedLiquidType = c.PumpedLiquidType,
                        IntakeType = c.IntakeType,
                        TypeName = c.Type != null ? c.Type.TypeName : null,
                        DryPump = c.Type != null && c.Type.DryPumps != null && c.Type.DryPumps.Any()
                        ? new JoinDryPumpDTO
                        {
                            SuctionHeight = c.Type.DryPumps.First().SuctionHeight,
                            InstalationType = c.Type.DryPumps.First().InstalationType
                        } : null,
                        SubPump = c.Type != null && c.Type.SubmersiblePumps != null && c.Type.SubmersiblePumps.Any()
                        ? new JoinSubPumpDTO
                        {
                            PotentialDepth = c.Type.SubmersiblePumps.First().PotentialDepth,
                            InstalationType = c.Type.SubmersiblePumps.First().InstalationType
                        } : null,
                        RequestId = c.RequestId,
                        WorkPumpsCount = c.WorkPumpsCount,
                        ReservePumpsCount = c.ReservePumpsCount,
                        LiquidTemperature = c.LiquidTemperature,
                        MineralParticlesSize = c.MineralParticlesSize,
                        MineralParticlesConcentration = c.MineralParticlesConcentration,
                        BigParticleExistance = c.BigParticleExistance,
                        SpecificWastes = c.SpecificWastes,
                        LiquidDensity = c.LiquidDensity,
                        RequiredPressure = c.RequiredPressure,
                        RequiredOutPressure = c.RequiredOutPressure,
                        PressureLoses = c.PressureLoses,
                        NetworkLength = c.NetworkLength,
                        PipesConditions = c.PipesConditions,
                        PumpDiameter = c.PumpDiameter,
                        GeodesicalMarks = c.GeodesicalMarks,
                        ExplosionProtection = c.ExplosionProtection,
                        ControlType = c.ControlType,
                        PowerCurrentType = c.PowerCurrentType,
                        WorkPower = c.WorkPower,
                        FrequencyConverter = c.FrequencyConverter,
                        PowerCableLength = c.PowerCableLength,
                        LiftingTransportEquipment = c.LiftingTransportEquipment,
                        FlushValve = c.FlushValve,
                        OtherLevelMeters = c.OtherLevelMeters,
                        OtherRequirements = c.OtherRequirements
                    })
                .Where(x => x.RequestId == requestId)
                .FirstOrDefaultAsync();
            return res;
        }
    }
}
