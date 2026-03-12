namespace MarketAnalysisWebApi.DTOs.FileStorageDTOS
{
    public class OfferFileCreateDTO
    {
        public Guid OfferId { get; set; }
        public IFormFile File { get; set; }
    }
}
