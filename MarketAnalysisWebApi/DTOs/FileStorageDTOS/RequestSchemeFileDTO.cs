namespace MarketAnalysisWebApi.DTOs.FileStorageDTOS
{
    public class RequestSchemeFileDTO
    {
        public Guid RequestId { get; set; }
        public IFormFile ? File { get; set; }
    }
}
