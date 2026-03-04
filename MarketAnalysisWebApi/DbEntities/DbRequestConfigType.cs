using MarketAnalysisWebApi.DbEntities.Base;

namespace MarketAnalysisWebApi.DbEntities
{
    public class DbRequestConfigType : DbBase
    {
        public string? ConfigTypeName { get; set; }

        public ICollection<DbProjectRequest>? ProjectRequests { get; set; }
    }
}
