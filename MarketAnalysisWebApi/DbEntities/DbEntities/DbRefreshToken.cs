using MarketAnalysisWebApi.DbEntities.Base;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketAnalysisWebApi.DbEntities.DbEntities
{
    public class DbRefreshToken : DbBase
    {
        [Required]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public DbUser? User { get; set; }

        [Required]
        public string? Token { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }

        public bool IsRevoked { get; set; }

        [MaxLength(65)] // IPv6 может быть длинным
        public string? IpAddress { get; set; }

        [MaxLength(500)]
        public string? UserAgent { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now.ToLocalTime();
    }
}
