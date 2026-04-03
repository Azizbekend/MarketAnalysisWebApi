using MarketAnalysisWebApi.DbEntities.Base;
using MarketAnalysisWebApi.DbEntities.DbEntities;
using Org.BouncyCastle.Asn1.Ocsp;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketAnalysisWebApi.DbEntities.Notifications
{
    public class DbNotification : DbBaseEntity
    {
        [Required]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public DbUser? User { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        [MaxLength(50)]
        public string Type { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now.ToUniversalTime().AddHours(3);

        public bool IsRead { get; set; } = false;

        public DateTime? ReadAt { get; set; }

        public string? DataJson { get; set; } // JSON сериализованные дополнительные данные
    }
}
