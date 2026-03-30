using MarketAnalysisWebApi.DbEntities.DbRequestConfigurations.KNS;

namespace MarketAnalysisWebApi.DTOs.KnsConfigDTOs
{
    public class CreateKnsRequestFullDTO
    {
        public Guid ? RequestFileId { get; set; } = Guid.Empty;
        public Guid RegionId { get; set; }
        public string? NameByProjectDocs { get; set; }
        public string? ObjectName { get; set; }
        public string? ObjectStage { get; set; }
        public string? ProjectDocsChapter { get; set; }
        public DateTime PublicationEndDate { get; set; }
        public string? LocationRegion { get; set; }
        public string? CustomerName { get; set; }
        public string? ProjectOrganizationName { get; set; }
        public string? ContactName { get; set; }
        public string? PhoneNumber { get; set; }
        public Guid UserId { get; set; }
        public Guid ConfigTypeId { get; set; }
        public double Perfomance { get; set; }
        public PerfomanceMeasureUnit Units { get; set; }
        public double RequiredPumpPressure { get; set; }
        public int ActivePumpsCount { get; set; }
        public int ReservePumpsCount { get; set; }
        public int PumpsToWarehouseCount { get; set; }
        public PumpEnvironment PType { get; set; }
        public int EnvironmentTemperature { get; set; }
        public bool ExplosionProtection { get; set; }
        public int SupplyPipelineDepth { get; set; }
        public int SupplyPipelineDiameter { get; set; }
        public PumpsStartupMethod startupMethod { get; set; }
        public PipelineMaterial SupplyPipelineMaterial { get; set; }
        public int SupplyPipelineDirectionInHours { get; set; }
        public int PressurePipelineDepth { get; set; }
        public int PressurePipelineDiameter { get; set; }
        public PipelineMaterial PressurePipelineMaterial { get; set; }
        public int PressurePipelineDirectionInHours { get; set; }
        public bool HasManyExitPressurePipelines { get; set; }
        public int ExpectedDiameterOfPumpStation { get; set; }
        public int ExpectedHeightOfPumpStation { get; set; }
        public int InsulatedHousingDepth { get; set; }
        public int PowerContactsToController { get; set; }
        public ControllerInstalationPlace Place { get; set; }

        public List<Guid> EquipmentGuidList { get; set; } = new();
    }
}
