using MarketAnalysisWebApi.DbEntities.DbRequestConfigurations.PUMP;

namespace MarketAnalysisWebApi.DTOs.PumpDTO
{
    public class JoinDryPumpDTO
    {
        public double SuctionHeight { get; set; }
        public InstalationType InstalationType { get; set; }
    }
}
