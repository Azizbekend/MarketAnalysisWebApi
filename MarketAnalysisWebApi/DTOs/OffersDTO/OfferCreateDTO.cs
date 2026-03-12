namespace MarketAnalysisWebApi.DTOs.OffersDTO
{
    public class OfferCreateDTO
    {
        public string? OfferNumber { get; set; }
        public string? NameByProject { get; set; } 
        public string? NameBySupplier { get; set; } 
        public double CurrentPriceNDS { get; set; }
        public double CurrentPriceNoNDS { get; set; }
        public DateTime SupportingDocumentDate { get; set; }
        public string? WarehouseLocation { get; set; }
        public string? SupplierSiteURL { get; set; }
        public string? ManufacturerCountry { get; set; }
        public Guid BussinessAccId { get; set; }
        public Guid RequestId { get; set; }
    }
}
