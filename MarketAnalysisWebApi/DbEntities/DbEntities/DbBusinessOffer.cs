using MarketAnalysisWebApi.DbEntities.Base;
using MarketAnalysisWebApi.DbEntities.FileStorages;
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
        public Guid RequestId { get; set; }
        [ForeignKey(nameof(RequestId))]
        public DbProjectRequest? Request { get; set; }

        public Guid BussinessAccId { get; set; }
        [ForeignKey(nameof(BussinessAccId))]
        public DbBusinessAccount? BussinessAccount { get; set; }


        public Guid? OfferFileId { get; set; }
        [ForeignKey(nameof(OfferFileId))]
        public DbBusinessOfferFileModel? BusinesOfferFile { get; set; }

        public Guid? PassportFileId { get; set; }
        [ForeignKey(nameof(PassportFileId))]
        public DbEquipmentPassportFile? PassportFile  { get; set; }
        public Guid? CertificateFileId { get; set; }
        [ForeignKey(nameof(CertificateFileId))]
        public DbEquipmentCertificateFileModel? CertificateFile { get; set; }
        public Guid? PlanFileId { get; set; }
        [ForeignKey(nameof(PlanFileId))]
        public DbPlanFile? PlanFile { get; set; }


    }
}
