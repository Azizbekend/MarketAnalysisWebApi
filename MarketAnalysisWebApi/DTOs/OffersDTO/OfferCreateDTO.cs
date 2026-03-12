namespace MarketAnalysisWebApi.DTOs.OffersDTO
{
    public class OfferCreateDTO
    {
        public string? NameByProject { get; set; } 
        public string? NameBySupplier { get; set; } 
        public double CurrentPriceNDS { get; set; } 
        public string? WarehouseLocation { get; set; }
        public string? SupplierSiteURL { get; set; }
        public Guid BussinessAccId { get; set; }
        public Guid RequestId { get; set; }
    }
}
