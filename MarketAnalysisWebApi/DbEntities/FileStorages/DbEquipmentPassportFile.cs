using MarketAnalysisWebApi.DbEntities.Base;

namespace MarketAnalysisWebApi.DbEntities.FileStorages
{
    public class DbEquipmentPassportFile : DbBase
    {
        public string? FileName { get; set; }
        public string? ContentType { get; set; }
        public long FileSize { get; set; }
        public byte[]? FileData { get; set; }
    }
}
