using MarketAnalysisWebApi.DbEntities.Base;
using System.Text.Json.Serialization;

namespace MarketAnalysisWebApi.DbEntities.DbRequestConfigurations.PUMP
{
    public class DbPumpType : DbBaseEntity
    {
        public string? TypeName { get; set; }
        [JsonIgnore]
        public ICollection<DbPumpConfiguration>? PumpCongigs { get; set; }
        [JsonIgnore]
        public ICollection<DbDryPump> ? DryPumps { get; set; }
        [JsonIgnore]
        public ICollection<DbSubmersiblePump> ? SubmersiblePumps { get; set; }
    }
}
