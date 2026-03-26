using MarketAnalysisWebApi.DbEntities.Base;

namespace MarketAnalysisWebApi.DbEntities.DbRequestConfigurations
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
    public class DbPumpConfiguration : DbBase
    {
        public LiquidType PumpedLiquidType { get; set; }
        public double PumpEfficiency { get; set; }
        public double LiquidTemperature { get; set; }
        public double MineralParticlesSize { get; set; }
        public double MineralParticlesConcentration { get; set; }
        public bool BigParticleExistance { get; set; }
        public string? SpecificWastes { get; set; } // если есть специфические отходы, прописать через запятую
        public double LiquidDensity  { get; set; }
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
