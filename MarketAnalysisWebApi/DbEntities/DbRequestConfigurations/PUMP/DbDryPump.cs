using MarketAnalysisWebApi.DbEntities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketAnalysisWebApi.DbEntities.DbRequestConfigurations.PUMP
{
    public class DbDryPump : DbBaseEntity
    {
        public Guid PumpTypeId { get; set; }
        [ForeignKey(nameof(PumpTypeId))]
        public DbPumpType? Type { get; set; }
        public double SuctionHeight { get; set; }
        public InstalationType InstalationType { get; set; }        
    }
}
