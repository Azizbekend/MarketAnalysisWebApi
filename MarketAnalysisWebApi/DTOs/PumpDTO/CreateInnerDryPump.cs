using MarketAnalysisWebApi.DbEntities.DbEntities;
using MarketAnalysisWebApi.DbEntities.DbRequestConfigurations.PUMP;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketAnalysisWebApi.DTOs.PumpDTO
{
    public class CreateInnerDryPump
    {
        public Guid PumpTypeId { get; set; }
        public Guid RequestId { get; set; }
        public double SuctionHeight { get; set; }
        public InstalationType InstalationType { get; set; }
    }
}
