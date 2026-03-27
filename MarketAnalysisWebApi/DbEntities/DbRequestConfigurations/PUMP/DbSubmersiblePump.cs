using MarketAnalysisWebApi.DbEntities.Base;
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

    public class DbSubmersiblePump : DbBase
    {
        public Guid PumpTypeId { get; set; }
        [ForeignKey(nameof(PumpTypeId))]
        public DbPumpType? Type { get; set; }
        public double PotentialDepth { get; set; }
        public InstalationType InstalationType { get; set; }

        

    }
}
