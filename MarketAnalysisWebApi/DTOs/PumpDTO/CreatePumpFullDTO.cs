using MarketAnalysisWebApi.DbEntities.DbRequestConfigurations.PUMP;

namespace MarketAnalysisWebApi.DTOs.PumpDTO
{
    public class CreatePumpFullDTO
    {
        public Guid RequestFileId { get; set; }
        public string? NameByProjectDocs { get; set; }
        public string? ObjectName { get; set; }
        public string? LocationRegion { get; set; }
        public string? CustomerName { get; set; }
        public string? ProjectOrganizationName { get; set; }
        public string? ContactName { get; set; }
        public string? PhoneNumber { get; set; }
        public Guid UserId { get; set; }
        public Guid ConfigTypeId { get; set; }
        public Guid PumpTypeId { get; set; }
        public LiquidType PumpedLiquidType { get; set; }
        public double PumpEfficiency { get; set; }
        public LiquidsIntakeType IntakeType { get; set; }

        public int WorkPumpsCount { get; set; }
        public int ReservePumpsCount { get; set; }
        public double LiquidTemperature { get; set; }
        public double MineralParticlesSize { get; set; }
        public double MineralParticlesConcentration { get; set; }
        public bool BigParticleExistance { get; set; }
        public string? SpecificWastes { get; set; } // если есть специфические отходы, прописать через запятую
        public double LiquidDensity { get; set; }
        public double RequiredPressure { get; set; }
        public double RequiredOutPressure { get; set; }
        public double PressureLoses { get; set; }
        public double NetworkLength { get; set; }
        public NetworkConditions PipesConditions { get; set; }
        public double PumpDiameter { get; set; }
        public string? GeodesicalMarks { get; set; }
        public bool ExplosionProtection { get; set; }
        public string? ControlType { get; set; }
        public string? PowerCurrentType { get; set; }
        public double WorkPower { get; set; }
        public bool FrequencyConverter { get; set; }
        public double PowerCableLength { get; set; }
        public bool LiftingTransportEquipment { get; set; }
        public bool FlushValve { get; set; }
        public bool OtherLevelMeters { get; set; }
        public string? OtherRequirements { get; set; }
        public double HeightOrDepth { get; set; }
        public InstalationType InstalationType { get; set; }
    }
}
