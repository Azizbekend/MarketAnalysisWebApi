namespace MarketAnalysisWebApi.DTOs.FileStorageDTOS
{
    public class EquipmentCertificateCreateDTO
    {
        public Guid OfferId { get; set; }
        public IFormFile CertificateFile { get; set; }
    }
}
