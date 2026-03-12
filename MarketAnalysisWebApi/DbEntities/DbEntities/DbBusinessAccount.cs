using System.ComponentModel.DataAnnotations.Schema;

namespace MarketAnalysisWebApi.DbEntities.DbEntities
{
    public class DbBusinessAccount
    {
        public Guid Id { get; set; }
        public double Coins { get; set; }
        [ForeignKey("UsersTable")]
        public Guid UserId { get; set; }
        public DbUser? User { get; set; }

    }
}
