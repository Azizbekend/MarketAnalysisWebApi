using MarketAnalysisWebApi.DbEntities.Base;
using MarketAnalysisWebApi.DbEntities.DbRequestConfigurations.KNS;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MarketAnalysisWebApi.DbEntities.DbEntities
{
    public enum RequestStatus
    {
        New,
        Moderation,
        Rejected,
        Published
    }
    public class DbProjectRequest : DbBase
    {      public string? InnerId { get; set; }
        [Required]
        public string? NameByProjectDocs { get; set; }
        [Required]
        public string? ObjectName { get; set; }
        [Required]
        [MaxLength(50)]
        public string? LocationRegion { get; set; }
        [Required]
        public string? CustomerName { get; set; }
        [Required]
        public string? ProjectOrganizationName { get; set; }
        [Required]
        public string? ContactName { get; set; }
        [Required]
        [MaxLength(25)]
        public string? PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now.ToUniversalTime().AddHours(3);
        public RequestStatus Status { get; set; } = RequestStatus.New;
        public bool IsArchived { get; set; } = false;
        public Guid UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public DbUser? User { get; set; }

        public Guid ConfigTypeId { get; set; }
        [ForeignKey(nameof(ConfigTypeId))]
        public DbRequestConfigType? RequestConfigType { get; set; }
  

        [JsonIgnore]
        public ICollection<DbKnsConfiguration>? KnsConfigs { get; set; }
        [JsonIgnore]
        public ICollection<DbEquipRequest>? EquipRequest { get; set; }
        [JsonIgnore]
        public ICollection<DbFavoriteRequest>? FavoriteRequests { get; set; }
        [JsonIgnore]
        public ICollection<DbAccountRequest>? AccountRequests { get; set; }
    }
}
