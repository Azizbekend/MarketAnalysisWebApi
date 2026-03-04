using MarketAnalysisWebApi.DbEntities.Base;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketAnalysisWebApi.DbEntities
{
    public class DbCompany : DbBase
    {
        [Required]
        [MaxLength(255)]
        public string? FullCompanyName { get; set; }
        [MaxLength(50)]
        public string? ShortCompanyName { get; set; }
        [MaxLength(50)]
        public string? INN { get; set; }
        [MaxLength(50)]
        public string? KPP { get; set; }
        public string? JurAdress { get; set; }

        public byte[]? CompanyLogoFile { get; set; }
        public Guid CompanyTypeId { get; set; }
        [ForeignKey(nameof(CompanyTypeId))]
        public DbCompanyType? CompanyType { get; set; }

        public ICollection<DbSupplierUserCompany>? CompanyUsersLinks { get; set; }
    }
}
