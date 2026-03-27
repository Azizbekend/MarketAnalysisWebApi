using MarketAnalysisWebApi.DbEntities.Base;

namespace MarketAnalysisWebApi.DbEntities.DbRequestConfigurations.PUMP
{
    public class DbPumpType : DbBaseEntity
    {
        public string? TypeName { get; set; }
        public ICollection<DbPumpConfiguration>? PumpCongigs { get; set; }
        public ICollection<DbDryPump> ? DryPumps { get; set; }
        public ICollection<DbSubmersiblePump> ? SubmersiblePumps { get; set; }
    }
}
