using MarketAnalysisWebApi.DbEntities.Base;
using MarketAnalysisWebApi.DbEntities.DbEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketAnalysisWebApi.DbEntities.DbRequestConfigurations.PUMP
{
    public class DbDryPump : DbBaseEntity
    {
        public Guid PumpTypeId { get; set; }
        [ForeignKey(nameof(PumpTypeId))]
        public DbPumpType? Type { get; set; }
        public Guid RequestId { get; set; }
        [ForeignKey(nameof(RequestId))]
        public DbProjectRequest ? Request { get; set; }
        public double SuctionHeight { get; set; }
        public InstalationType InstalationType { get; set; }        
    }
}
