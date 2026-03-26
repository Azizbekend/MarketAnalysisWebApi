namespace MarketAnalysisWebApi.DbEntities.DbRequestConfigurations
{
    public enum LiquidType
    {
        HouseholdLiquids = 1,
        FacilityLiquids,
        RainIceLiquids
    }
    public class DbPumpConfiguration
    {
        public LiquidType PumpedLiquidType { get; set; }
        public double PumpEfficiency { get; set; }
        public double LiquidTemperature { get; set; }
        public double MineralParticlesSize { get; set; }
        public double MineralParticlesConcentration { get; set; }
        public bool BigParticleExistance { get; set; }
        public string? SpecificWastes { get; set; } // если есть специфические отходы, прописать через запятую
    }
}
