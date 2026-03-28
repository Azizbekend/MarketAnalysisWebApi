using MarketAnalysisWebApi.DbEntities.DbRequestConfigurations.PUMP;

namespace MarketAnalysisWebApi.DTOs.PumpDTO
{
    public class JoinSubPumpDTO
    {
        public double PotentialDepth { get; set; }
        public InstalationType InstalationType { get; set; }
    }
}
