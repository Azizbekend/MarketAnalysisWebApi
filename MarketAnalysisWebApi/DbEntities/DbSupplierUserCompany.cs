using MarketAnalysisWebApi.DbEntities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketAnalysisWebApi.DbEntities
{
    public class DbSupplierUserCompany : DbBase
    {
        public Guid CompanyId { get; set; }
        [ForeignKey(nameof(CompanyId))]
        public DbCompany? SupplierCompany { get; set; }
        public Guid SupplierUserId { get; set; }
        [ForeignKey(nameof(SupplierUserId))]
        public DbUser? SupplierUser { get; set; }
    }
}
