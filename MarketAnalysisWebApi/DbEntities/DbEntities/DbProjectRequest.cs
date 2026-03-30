using MarketAnalysisWebApi.DbEntities.Base;
using MarketAnalysisWebApi.DbEntities.DbRequestConfigurations.KNS;
using MarketAnalysisWebApi.DbEntities.DbRequestConfigurations.PUMP;
using MarketAnalysisWebApi.DbEntities.FileStorages;
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
    public class DbProjectRequest : DbBaseEntity
    {      
        public string? InnerId { get; set; }
        [Required]
        public string? NameByProjectDocs { get; set; }
        [Required]
        public string? ObjectName { get; set; }
        [Required]
        public string ? ObjectStage { get; set; }
        public string ? ProjectDocsChapter { get; set; }
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
        public DateTime PublicationEndDate { get; set; }
        public RequestStatus Status { get; set; } = RequestStatus.New;
        public Guid ? RegionId { get; set; }
        [ForeignKey(nameof(RegionId))]
        public DbRegion ? Region { get; set; }
        public bool IsArchived { get; set; } = false;
        public Guid ? FileId { get; set; }
        [ForeignKey(nameof(FileId))]
        public DbRequestFileModel ? File { get; set; }
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
        [JsonIgnore]
        public ICollection<DbPumpConfiguration> ? PumpConfigurations { get; set; }
        [JsonIgnore]
        public ICollection<DbDryPump> ? DryPumps { get; set; }
        [JsonIgnore]
        public ICollection<DbSubmersiblePump> ? SubmersiblePumps { get; set; }
        [JsonIgnore]
        public ICollection<DbBusinessOffer> ? Offers { get; set; }
    }
}
