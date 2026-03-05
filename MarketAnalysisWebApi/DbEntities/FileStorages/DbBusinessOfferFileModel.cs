namespace MarketAnalysisWebApi.DbEntities.FileStorages
{
    public class DbBusinessOfferFileModel
    {
        public string? FileName { get; set; }
        public string? ContentType { get; set; }
        public long FileSize { get; set; }
        public byte[]? FileData { get; set; }
    }
}
