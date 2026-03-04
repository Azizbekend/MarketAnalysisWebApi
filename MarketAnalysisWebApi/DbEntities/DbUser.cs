using MarketAnalysisWebApi.DbEntities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MarketAnalysisWebApi.DbEntities
{
    public class DbUser : DbBase
    {
        [MaxLength(255)]
        public string? FullName { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }
        public string?  Password { get; set; }
        public Guid RoleId { get; set; }
        [ForeignKey(nameof(RoleId))]
        public DbUserRole? UserRole { get; set; }

        public ICollection<DbSupplierUserCompany>? CompanyUsersLinks { get; set; }
    }
}
