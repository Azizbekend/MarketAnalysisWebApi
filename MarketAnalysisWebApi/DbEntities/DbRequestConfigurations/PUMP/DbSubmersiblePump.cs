using MarketAnalysisWebApi.DbEntities.Base;
using MarketAnalysisWebApi.DbEntities.DbEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketAnalysisWebApi.DbEntities.DbRequestConfigurations.PUMP
{
    public enum InstalationType 
    {
        HalfStable = 1,
        Portable,
        Vertical,
        Horizontal
    }
    
    public enum NetworkConditions
    {
        New = 1,
        TenYears,
        Old
    }

    public class DbSubmersiblePump : DbBaseEntity
    {
        public Guid PumpTypeId { get; set; }
        [ForeignKey(nameof(PumpTypeId))]
        public DbPumpType? Type { get; set; }
        public Guid RequestId { get; set; }
        [ForeignKey(nameof(RequestId))]
        public DbProjectRequest? Request { get; set; }
        public double PotentialDepth { get; set; }
        public InstalationType InstalationType { get; set; }

        

    }
}
