using MarketAnalysisWebApi.DbEntities.DbEntities;
using MarketAnalysisWebApi.DbEntities.DbRequestConfigurations.PUMP;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketAnalysisWebApi.DTOs.PumpDTO
{
    public class JoinPumpConfigDTO
    {
        public Guid RequestId { get; set; }
        public string ? TypeName { get; set; }
        public double PumpEfficiency { get; set; }
        public LiquidType PumpedLiquidType { get; set; }
        public LiquidsIntakeType IntakeType { get; set; }
        public JoinDryPumpDTO ? DryPump { get; set; }
        public JoinSubPumpDTO ? SubPump { get; set; }
        public string ? InstalationPlace { get; set; }
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
    }
}
