using MarketAnalysisWebApi.DbEntities.Base;
using MarketAnalysisWebApi.DbEntities.DbRequestConfigurations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketAnalysisWebApi.DbEntities
{
    public class DbProjectRequest : DbBase
    {
        [Required]
        public string? ObjectName { get; set; }
        [Required]
        public string? CustomerName { get; set; }
        [Required]
        public string? ContactName { get; set; }
        [Required]
        [MaxLength(25)]
        public string? PhoneNumber { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public DbUser? User { get; set; }

        public Guid ConfigTypeId { get; set; }
        [ForeignKey(nameof(ConfigTypeId))]
        public DbRequestConfigType? RequestConfigType { get; set; }


        public ICollection<DbKnsConfig>? KnsConfigs { get; set; }

    }
}
