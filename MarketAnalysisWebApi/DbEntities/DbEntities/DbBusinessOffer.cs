using MarketAnalysisWebApi.DbEntities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketAnalysisWebApi.DbEntities.DbEntities
{
    public class DbBusinessOffer : DbBase
    {
        public string? NameByProject { get; set; } //from request name
        public string? NameBySupplier { get; set; } // from supplier BO form
        public double CurrentPriceNDS { get; set; } // from form BO
        public string? WarehouseLocation { get; set; }
        public string? SupplierSiteURL { get; set; }
        public Guid CompanyId { get; set; }
        [ForeignKey(nameof(CompanyId))]
        public DbCompany? Company { get; set; }

    }
}
