using MarketAnalysisWebApi.DbEntities.Base;
using System.Text.Json.Serialization;

namespace MarketAnalysisWebApi.DbEntities.DbEntities
{
    public class DbRegion : DbBaseEntity
    {
        public string ? RegionName { get; set; }
        [JsonIgnore]
        public ICollection<DbProjectRequest> ? PRequests { get; set; }
    }
}
