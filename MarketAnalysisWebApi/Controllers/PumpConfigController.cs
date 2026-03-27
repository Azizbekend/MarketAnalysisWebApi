using MarketAnalysisWebApi.DTOs.PumpDTO;
using MarketAnalysisWebApi.DTOs.RequestDTOs;
using MarketAnalysisWebApi.Repos.PumpConfigRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketAnalysisWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PumpConfigController : ControllerBase
    {
        private readonly IPumpConfigRepo _pumpConfigRepo;

        public PumpConfigController(IPumpConfigRepo pumpConfigRepo)
        {
            _pumpConfigRepo = pumpConfigRepo;
        }
        [HttpGet("pump/types/all")]
        public async Task<IActionResult> GetTypes()
        {
            var res = await _pumpConfigRepo.GetTypes();
            return Ok(res);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreatePumpRequest(CreatePumpFullDTO dto)
        {
            var pumpRequest = new CreateInnerRequestDTO
            {
                NameByProjectDocs = dto.NameByProjectDocs,
                ObjectName = dto.ObjectName,

                CustomerName = dto.CustomerName,
                ProjectOrganizationName = dto.ProjectOrganizationName,
                ConfigTypeId = dto.ConfigTypeId,
                ContactName = dto.ContactName,
                PhoneNumber = dto.PhoneNumber,
                UserId = dto.UserId                
            };
            var requestId = await _pumpConfigRepo.CreatePumpRequestAsync(pumpRequest);
            var pumpConfig = new CreateInnerPumpConfig
            {
                RequestId = requestId,
                PumpTypeId = dto.PumpTypeId,
                PumpedLiquidType = dto.PumpedLiquidType,
                PumpEfficiency = dto.PumpEfficiency,
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
                PumpDiameter = dto.PumpDiameter,
                GeodesicalMarks = dto.GeodesicalMarks,
                ExplosionProtection = dto.ExplosionProtection,
                ControlType = dto.ControlType,
                PowerCurrentType = dto.PowerCurrentType,
                WorkPower = dto.WorkPower,
                FrequencyConverter = dto.FrequencyConverter,
                PowerCableLength = dto.PowerCableLength,
                LiftingTransportEquipment = dto.LiftingTransportEquipment,
                FlushValve = dto.FlushValve,    
                OtherLevelMeters = dto.OtherLevelMeters,
                OtherRequirements = dto.OtherRequirements
            };
            var pumpConfigId = await _pumpConfigRepo.CreatePumpConfig(pumpConfig);
            if (dto.PumpTypeId == Guid.Parse("019d2f11-8873-7d95-82a1-289e4d289917")) //dry
            {
                var dryPump = new CreateInnerDryPump
                {
                    RequestId = requestId,
                    PumpTypeId = dto.PumpTypeId,
                    InstalationType = dto.InstalationType,
                    SuctionHeight = dto.HeightOrDepth
                };
                var res = await _pumpConfigRepo.CreateDryPump(dryPump);
            }
            else
            {
                var subPump = new CreateInnerSubPump
                {
                    RequestId = requestId,
                    PumpTypeId = dto.PumpTypeId,
                    InstalationType = dto.InstalationType,
                    PotentialDepth = dto.HeightOrDepth
                };
                var res = await _pumpConfigRepo.CreateSubPump(subPump);
            }
            return Ok(pumpConfigId);
        }
    }
}
