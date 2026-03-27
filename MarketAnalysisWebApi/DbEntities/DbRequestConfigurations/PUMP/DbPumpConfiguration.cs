using MarketAnalysisWebApi.DbEntities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketAnalysisWebApi.DbEntities.DbRequestConfigurations.PUMP
{
    public enum LiquidType
    {
        HouseholdLiquids = 1,
        FacilityLiquids,
        RainIceLiquids
    }

    public enum LiquidsIntakeType
    {
        Even = 1,
        Periodic,
        Uneven
    }
    public class DbPumpConfiguration : DbBaseEntity
    {
        public Guid PumpTypeId { get; set; }
        [ForeignKey(nameof(PumpTypeId))]
        public DbPumpType? Type { get; set; }
        public LiquidType PumpedLiquidType { get; set; }
        public double PumpEfficiency { get; set; }
        public double LiquidTemperature { get; set; }
        public double MineralParticlesSize { get; set; }
        public double MineralParticlesConcentration { get; set; }
        public bool BigParticleExistance { get; set; }
        public string? SpecificWastes { get; set; } // если есть специфические отходы, прописать через запятую
        public double LiquidDensity  { get; set; }
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
