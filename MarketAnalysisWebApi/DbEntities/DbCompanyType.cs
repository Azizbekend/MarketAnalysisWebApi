using MarketAnalysisWebApi.DbEntities.Base;

namespace MarketAnalysisWebApi.DbEntities
{
    public class DbCompanyType : DbBase
    {
        public string? TypeName { get; set; }

        public ICollection<DbCompany>? Companies { get; set; }
    }
}
