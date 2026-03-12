namespace MarketAnalysisWebApi.DTOs.FileStorageDTOS
{
    public class PlanFileCreateDTO
    {
        public Guid OfferId { get; set; }
        public IFormFile PlanFile { get; set; }
    }
}
