using MarketAnalysisWebApi.DbEntities.Base;
using MarketAnalysisWebApi.DbEntities.DbEntities;

namespace MarketAnalysisWebApi.DbEntities.FileStorages
{
    public class DbPlanFile : DbBaseEntity
    {
        public string? FileName { get; set; }
        public string? ContentType { get; set; }
        public long FileSize { get; set; }
        public byte[]? FileData { get; set; }

        public ICollection<DbBusinessOffer>? BusinesOffers { get; set; }
    }
}
