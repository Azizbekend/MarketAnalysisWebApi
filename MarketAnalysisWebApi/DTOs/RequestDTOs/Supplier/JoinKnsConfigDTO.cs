using MarketAnalysisWebApi.DbEntities.DbRequestConfigurations.KNS;

namespace MarketAnalysisWebApi.DTOs.RequestDTOs.Supplier
{
    public class JoinKnsConfigDTO
    {
        public double Efficiency { get; set; }
        public PerfomanceMeasureUnit Untis { get; set; }
    }
}
