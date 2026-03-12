using MarketAnalysisWebApi.DTOs.KnsConfigDTOs;
using MarketAnalysisWebApi.DTOs.KnsCongigDTOs;
using MarketAnalysisWebApi.Repos.KnsConfiGRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace MarketAnalysisWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KnsRequestController : ControllerBase
    {
        private readonly IKnsConfigRepo _knsConfigRepo;

        public KnsRequestController(IKnsConfigRepo knsConfigRepo)
        {
            _knsConfigRepo = knsConfigRepo;
        }
        [HttpGet("knsConfig/equipments")]
        public async Task<IActionResult> GetKnsEquipment()
        {
            var res = await _knsConfigRepo.GetKnsConfigEquipment(Guid.Parse("019cdcd9-1892-7f3a-955c-3503ede15a6d"));
            return Ok(res);
        }

        [HttpGet("knsConfig/current")]
        public async Task<IActionResult> GetCurrentKnsCongig(Guid requestId)
        {
            try
            {
                var res = await _knsConfigRepo.GetKnsConfig(requestId);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("knsConfig/equipment/current")]
        public async Task<IActionResult> GetCurrentKnsEquipment(Guid requestId)
        {
            try
            {
                var res = await _knsConfigRepo.GetCurrentKnsEquipment(requestId);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost("create/new")]
        public async Task<IActionResult> CreateKnsRequest(CreateKnsRequestFullDTO dto)
        {
            try
            {
                var request = new CreateKnsInnerRequestDTO()
                {
                    NameByProjectDocs = dto.NameByProjectDocs,
                    ObjectName = dto.ObjectName,
                    LocationRegion = dto.LocationRegion,
                    CustomerName = dto.CustomerName,
                    ContactName = dto.ContactName,
                    PhoneNumber = dto.PhoneNumber,
                    UserId = dto.UserId,
                    ConfigTypeId = dto.ConfigTypeId
                };
                var resId = await _knsConfigRepo.CreateKnsRequest(request);
                var config = new CreateKnsInnerConfigDTO()
                {
                    Perfomance = dto.Perfomance,
                    Units = dto.Units,
                    RequiredPumpPressure = dto.RequiredPumpPressure,
                    ActivePumpsCount = dto.ActivePumpsCount,
                    ReservePumpsCount = dto.ReservePumpsCount,
                    PumpsToWarehouseCount = dto.PumpsToWarehouseCount,
                    PType = dto.PType,
                    EnvironmentTemperature = dto.EnvironmentTemperature,
                    ExplosionProtection = dto.ExplosionProtection,
                    SupplyPipelineDepth = dto.SupplyPipelineDepth,
                    SupplyPipelineDiameter = dto.SupplyPipelineDiameter,
                    startupMethod = dto.startupMethod,
                    SupplyPipelineDirectionInHours = dto.SupplyPipelineDirectionInHours,
                    PressurePipelineDepth = dto.PressurePipelineDepth,
                    PressurePipelineDiameter = dto.PressurePipelineDiameter,
                    PressurePipelineMaterial = dto.PressurePipelineMaterial,
                    PressurePipelineDirectionInHours = dto.PressurePipelineDirectionInHours,
                    HasManyExitPressurePipelines = dto.HasManyExitPressurePipelines,
                    ExpectedDiameterOfPumpStation = dto.ExpectedDiameterOfPumpStation,
                    ExpectedHeightOfPumpStation = dto.ExpectedHeightOfPumpStation,
                    InsulatedHousingDepth = dto.InsulatedHousingDepth,
                    PowerContactsToController = dto.PowerContactsToController,
                    Place = dto.Place,
                    RequestId = resId
                };
                await _knsConfigRepo.CreateKnsConfig(config);
                await _knsConfigRepo.CreateKnsEquipment(resId, dto.EquipmentGuidList);
                return Ok(resId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
