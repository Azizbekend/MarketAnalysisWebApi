using MarketAnalysisWebApi.DbEntities.Base;
using MarketAnalysisWebApi.DbEntities.DbEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketAnalysisWebApi.DbEntities.FileStorages
{
    public class DbOtherOfferFileModel : DbBaseEntity
    {
        public Guid OfferId { get; set; }
        [ForeignKey(nameof(OfferId))]
        public DbBusinessOffer Offer { get; set; }

        public int MyProperty { get; set; }
        public string? FileName { get; set; }
        public string? ContentType { get; set; }
        public long FileSize { get; set; }
        public byte[]? FileData { get; set; }
    }
}
