using MarketAnalysisWebApi.DbEntities.Base;
using MarketAnalysisWebApi.DbEntities.DbRequestConfigurations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketAnalysisWebApi.DbEntities.DbEntities
{
    public class DbEquipRequest : DbBaseEntity
    {
        public Guid RequestId { get; set; }
        [ForeignKey(nameof(RequestId))]
        public DbProjectRequest? ProjectRequest { get; set; }
        public Guid EquipmentId { get; set; }
        [ForeignKey(nameof(EquipmentId))]
        public DbEquipment? Equipment { get; set; }
    }
}
