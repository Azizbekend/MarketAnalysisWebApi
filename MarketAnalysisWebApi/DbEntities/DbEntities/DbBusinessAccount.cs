using MarketAnalysisWebApi.DbEntities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketAnalysisWebApi.DbEntities.DbEntities
{
    public class DbBusinessAccount : DbBase
    {
        public double Coins { get; set; } = 10;
        [ForeignKey("UsersTable")]
        public Guid UserId { get; set; }
        public DbUser? User { get; set; }

        public ICollection <DbAccountRequest>? AccountRequests { get; set; } 
    }
}
