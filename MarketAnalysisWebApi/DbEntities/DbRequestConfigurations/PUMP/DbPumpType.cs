using MarketAnalysisWebApi.DbEntities.Base;

namespace MarketAnalysisWebApi.DbEntities.DbRequestConfigurations.PUMP
{
    public class DbPumpType : DbBase
    {
        public string? TypeName { get; set; }
        public ICollection<DbPumpConfiguration>? PumpCongigs { get; set; }
    }
}
