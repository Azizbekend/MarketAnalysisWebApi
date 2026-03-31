using MarketAnalysisWebApi.DbEntities.DbRequestConfigurations.KNS;
using Microsoft.AspNetCore.Identity;

namespace MarketAnalysisWebApi.DTOs.RequestDTOs.Supplier
{
    public class JoinKnsConfigDTO
    {
        public double Efficiency { get; set; }
        public PerfomanceMeasureUnit Untis { get; set; }
        public string  InstalationPlace { get; set; }
    }
}
