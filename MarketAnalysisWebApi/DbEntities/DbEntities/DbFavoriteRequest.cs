using MarketAnalysisWebApi.DbEntities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketAnalysisWebApi.DbEntities.DbEntities
{
    public class DbFavoriteRequest : DbBase
    {
        public Guid UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public DbUser? User { get; set; }
        public Guid RequestId { get; set; }
        [ForeignKey(nameof(RequestId))]
        public DbProjectRequest? request { get; set; }
    }
}
