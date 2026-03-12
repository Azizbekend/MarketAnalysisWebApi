using MarketAnalysisWebApi.DbEntities.DbEntities;
using MarketAnalysisWebApi.DbEntities.FileStorages;
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketAnalysisWebApi.DTOs.OffersDTO
{
    public class FullOfferDTO
    {
        public string? OffersNumber { get; set; }
        public string? NameByProject { get; set; } //from request name
        public string? NameBySupplier { get; set; } // from supplier BO form
        public double CurrentPriceNDS { get; set; } // from form BO
        public double CurrentPriceNoNDS { get; set; } // from form BO
        public DateTime SupportingDocumentDate { get; set; }
        public string? WarehouseLocation { get; set; }
        public string? ManufacturerCountry { get; set; }
        public string? SupplierSiteURL { get; set; }
        public string? FullCompanyName { get; set; }
        public string? INN { get; set; }
        public string? KPP { get; set; }





        public Guid? OfferFileId { get; set; }
        public Guid? PassportFileId { get; set; }
        public Guid? CertificateFileId { get; set; }
        public Guid? PlanFileId { get; set; }

    }
}
