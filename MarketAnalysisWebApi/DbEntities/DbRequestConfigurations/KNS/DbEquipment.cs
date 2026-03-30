using MarketAnalysisWebApi.DbEntities.Base;
using MarketAnalysisWebApi.DbEntities.DbEntities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MarketAnalysisWebApi.DbEntities.DbRequestConfigurations.KNS
{
    public class DbEquipment : DbBaseEntity
    {
        public string? Name { get; set; }
        public Guid ConfigTypeId { get; set; }
        [ForeignKey(nameof(ConfigTypeId))]
        public DbRequestConfigType? Type { get; set; }
        [JsonIgnore]
        public ICollection<DbEquipRequest>? EquipRequests { get; set; }
    }
}
