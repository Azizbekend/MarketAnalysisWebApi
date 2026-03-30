namespace MarketAnalysisWebApi.DTOs.FileStorageDTOS
{
    public class OtherOfferFileCreateDTO
    {
        public Guid OfferId { get; set; }
        public IFormFile OfferFile { get; set; }
    }
}
