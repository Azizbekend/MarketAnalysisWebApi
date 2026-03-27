using MarketAnalysisWebApi.DbEntities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MarketAnalysisWebApi.DbEntities.DbEntities
{
    public class DbUser : DbBaseEntity
    {
        [MaxLength(255)]
        public string? FullName { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        [MaxLength(25)]
        public string? PhoneNumber { get; set; }
        public string?  Password { get; set; }
        public Guid RoleId { get; set; }
        [ForeignKey(nameof(RoleId))]
        public DbUserRole? UserRole { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now.ToUniversalTime().AddHours(3);

        public ICollection<DbSupplierUserCompany>? CompanyUsersLinks { get; set; }
        public ICollection<DbProjectRequest>? UsersRequests { get; set; }
        public ICollection<DbFavoriteRequest>? FavoriteRequests { get; set; }
        public ICollection<DbRefreshToken>? RefreshTokens { get; set; }
        public ICollection<DbBusinessAccount>? BusinessAccounts { get; set; }
    }
}
 