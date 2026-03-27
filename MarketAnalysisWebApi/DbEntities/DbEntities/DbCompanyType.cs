using MarketAnalysisWebApi.DbEntities.Base;

namespace MarketAnalysisWebApi.DbEntities.DbEntities
{
    public class DbCompanyType : DbBaseEntity
    {
        public string? TypeName { get; set; }

        public ICollection<DbCompany>? Companies { get; set; }
    }
}
