namespace MarketAnalysisWebApi.DTOs.FileStorageDTOS
{
    public class RequestFileSchemeUpdateDTO
    {
        public Guid RequestFileId { get; set; }
        public IFormFile RequestFile { get; set; }
    }
}
