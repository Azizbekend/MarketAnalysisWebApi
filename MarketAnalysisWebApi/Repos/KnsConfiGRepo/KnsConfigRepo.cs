using MarketAnalysisWebApi.DbEntities;
using MarketAnalysisWebApi.DbEntities.DbEntities;
using MarketAnalysisWebApi.DbEntities.DbRequestConfigurations;
using MarketAnalysisWebApi.DTOs.KnsCongigDTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;

namespace MarketAnalysisWebApi.Repos.KnsConfiGRepo
{
    public class KnsConfigRepo : IKnsConfigRepo
    {
        private readonly AppDbContext _context;

        public KnsConfigRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateKnsConfig(CreateKnsInnerConfigDTO dto)
        {
            var knsCongif = new DbKnsConfig
            {
                Perfomance = dto.Perfomance,
                Units = dto.Units,
                RequiredPumpPressure = dto.RequiredPumpPressure,
                ActivePumpsCount = dto.ActivePumpsCount,
                ReservePumpsCount = dto.ReservePumpsCount,
                PumpsToWarehouseCount = dto.PumpsToWarehouseCount,
                startupMethod = dto.startupMethod,
                PType = dto.PType,
                EnvironmentTemperature = dto.EnvironmentTemperature,
                ExplosionProtection = dto.ExplosionProtection,
                SupplyPipelineDepth = dto.SupplyPipelineDepth,
                SupplyPipelineDiameter = dto.SupplyPipelineDiameter,
                SupplyPipelineMaterial = dto.SupplyPipelineMaterial,
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
                RequestId = dto.RequestId
            };
            await _context.KnsConfigurations.AddAsync(knsCongif);
            await _context.SaveChangesAsync();
            return knsCongif.Id;

        }

        public async Task CreateKnsEquipment(Guid requestId, List<Guid> equipments)
        {
            foreach (var equip in equipments)
            {
                var relation = new DbEquipRequest
                {
                    RequestId = requestId,
                    EquipmentId = equip
                };
                _context.EquipRequestTable.Add(relation);
                await _context.SaveChangesAsync();

            }
        }

        public async Task<Guid> CreateKnsRequest(CreateKnsInnerRequestDTO dto)
        {
            var knsRequest = new DbProjectRequest
            {
                NameByProjectDocs = dto.NameByProjectDocs,
                ObjectName = dto.ObjectName,
                LocationRegion = dto.LocationRegion,
                CustomerName = dto.CustomerName,
                ContactName = dto.ContactName,
                PhoneNumber = dto.PhoneNumber,
                ConfigTypeId = dto.ConfigTypeId,
                UserId = dto.UserId,
                Status = RequestStatus.Published

            };
            await  _context.ProjectRequestsTable.AddAsync(knsRequest);
            await _context.SaveChangesAsync();
            return knsRequest.Id;

        }

        public async Task<ICollection<DbEquipment>> GetCurrentKnsEquipment(Guid request)
        {
            var prEqList = await _context.EquipRequestTable.Where(x => x.RequestId == request).ToListAsync();
            var list = new List<DbEquipment>();
            foreach (var eq in prEqList)
            {
                var res2 = await _context.EquipmentTable.FirstOrDefaultAsync(x => x.Id == eq.EquipmentId);
                list.Add(res2);
            }
            return list;

        }

        public async Task<DbKnsConfig> GetKnsConfig(Guid requestId)
        {
            var res = await _context.KnsConfigurations.FirstOrDefaultAsync(x => x.RequestId == requestId);
            if (res == null)
            {
                throw new Exception("Не корректный Id заявки!");

            }
            else
            {
                return res;
            }
        }

        public async Task<ICollection<DbEquipment>> GetKnsConfigEquipment(Guid typeId)
        {
            return await _context.EquipmentTable.Where(x => x.ConfigTypeId == typeId).ToListAsync();
        }
    }
}
