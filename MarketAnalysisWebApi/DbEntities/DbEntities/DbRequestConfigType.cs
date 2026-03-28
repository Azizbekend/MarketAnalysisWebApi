using MarketAnalysisWebApi.DbEntities.Base;
using System.Text.Json.Serialization;

namespace MarketAnalysisWebApi.DbEntities.DbEntities
{
    public class DbRequestConfigType : DbBaseEntity
    {
        public string? ConfigTypeName { get; set; }
        [JsonIgnore]
        public ICollection<DbProjectRequest>? ProjectRequests { get; set; }
        [JsonIgnore]
        public ICollection<DbEquipment>? Equipments { get; set; }
    }
}
