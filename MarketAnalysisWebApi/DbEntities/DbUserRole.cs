using MarketAnalysisWebApi.DbEntities.Base;

namespace MarketAnalysisWebApi.DbEntities
{
    public class DbUserRole : DbBase
    {
        public string? RoleName { get; set; }

        public ICollection<DbUser>? Users { get; set; }
    }
}
