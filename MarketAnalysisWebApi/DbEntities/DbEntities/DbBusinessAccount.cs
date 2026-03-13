using MarketAnalysisWebApi.DbEntities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketAnalysisWebApi.DbEntities.DbEntities
{
    public class DbBusinessAccount : DbBase
    {
        public double Coins { get; set; } = 1000;
        [ForeignKey("UsersTable")]
        public Guid UserId { get; set; }
        public DbUser? User { get; set; }



    }
}
