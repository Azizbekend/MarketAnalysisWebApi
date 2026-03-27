using MarketAnalysisWebApi.DbEntities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketAnalysisWebApi.DbEntities.DbEntities
{
    public class DbAccountRequest : DbBaseEntity
    {

        public Guid AccountId { get; set; }
        [ForeignKey(nameof(AccountId))]
        public DbBusinessAccount? account { get; set; }
        public Guid RequestId { get; set; }
        [ForeignKey(nameof(RequestId))]
        public DbProjectRequest? Request { get; set; }
        public string? Status { get; set; }
    }
}
