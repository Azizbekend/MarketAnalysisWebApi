using MarketAnalysisWebApi.DbEntities.Base;

namespace MarketAnalysisWebApi.DbEntities.DbEntities
{
    public class DbRequestConfigType : DbBaseEntity
    {
        public string? ConfigTypeName { get; set; }

        public ICollection<DbProjectRequest>? ProjectRequests { get; set; }
        public ICollection<DbEquipment>? Equipments { get; set; }
    }
}
