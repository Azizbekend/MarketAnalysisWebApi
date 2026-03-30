using MarketAnalysisWebApi.DbEntities;
using MarketAnalysisWebApi.DbEntities.DbEntities;
using MarketAnalysisWebApi.DbEntities.DbRequestConfigurations.KNS;
using MarketAnalysisWebApi.DTOs.KnsCongigDTOs;
using MarketAnalysisWebApi.DTOs.RequestDTOs;
using MarketAnalysisWebApi.Repos.BaseRepo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;

namespace MarketAnalysisWebApi.Repos.KnsConfiGRepo
{
    public class KnsConfigRepo : BaseRepo<DbKnsConfiguration>, IBaseRepo<DbKnsConfiguration>,IKnsConfigRepo
    {
        public KnsConfigRepo(AppDbContext appDbContext) : base(appDbContext) { }


        public async Task<Guid> CreateKnsConfig(CreateKnsInnerConfigDTO dto)
        {
            var knsCongif = new DbKnsConfiguration
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
            await _appDbContext.KnsConfigurations.AddAsync(knsCongif);
            await _appDbContext.SaveChangesAsync();
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
                _appDbContext.EquipRequestTable.Add(relation);
                await _appDbContext.SaveChangesAsync();

            }
        }

        public async Task<Guid> CreateKnsRequest(CreateInnerRequestDTO dto)
        {

            var knsRequest = new DbProjectRequest
            {
                NameByProjectDocs = dto.NameByProjectDocs,
                ObjectStage = dto.ObjectStage,
                ProjectDocsChapter = dto.ProjectDocsChapter,
                PublicationEndDate = dto.PublicationEndDate,
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
            await _appDbContext.ProjectRequestsTable.AddAsync(knsRequest);
            await _appDbContext.SaveChangesAsync();
            return knsRequest.Id;

        }

        public async Task<ICollection<DbEquipment>> GetCurrentKnsEquipment(Guid request)
        {
            var prEqList = await _appDbContext.EquipRequestTable.Where(x => x.RequestId == request).ToListAsync();
            var list = new List<DbEquipment>();
            foreach (var eq in prEqList)
            {
                var res2 = await _appDbContext.EquipmentTable.FirstOrDefaultAsync(x => x.Id == eq.EquipmentId);
                list.Add(res2);
            }
            return list;

        }

        public async Task<DbKnsConfiguration> GetKnsConfig(Guid requestId)
        {
            var res = await _appDbContext.KnsConfigurations.FirstOrDefaultAsync(x => x.RequestId == requestId);
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
            return await _appDbContext.EquipmentTable.Where(x => x.ConfigTypeId == typeId).ToListAsync();
        }


    }
}
