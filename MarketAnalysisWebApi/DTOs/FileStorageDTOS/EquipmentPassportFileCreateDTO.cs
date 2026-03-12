namespace MarketAnalysisWebApi.DTOs.FileStorageDTOS
{
    public class EquipmentPassportFileCreateDTO
    {
        public Guid OfferId { get; set; }
        public IFormFile PassportFile { get; set; }
    }
}
