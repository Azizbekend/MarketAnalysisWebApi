using MarketAnalysisWebApi.DbEntities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketAnalysisWebApi.DbEntities.DbEntities
{
    public class DbEquipment : DbBase
    {
        public string? Name { get; set; }
        public Guid ConfigTypeId { get; set; }
        [ForeignKey(nameof(ConfigTypeId))]
        public DbRequestConfigType? Type { get; set; }

        public ICollection<DbEquipRequest>? EquipRequests { get; set; }
    }
}
