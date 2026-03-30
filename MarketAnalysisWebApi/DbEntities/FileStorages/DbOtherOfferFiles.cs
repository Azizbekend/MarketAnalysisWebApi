using MarketAnalysisWebApi.DbEntities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketAnalysisWebApi.DbEntities.FileStorages
{
    public class DbOtherOfferFiles : DbBaseEntity
    {
        public Guid RequestId { get; set; }
        [ForeignKey(nameof(RequestId))]

        public int MyProperty { get; set; }
        public string? FileName { get; set; }
        public string? ContentType { get; set; }
        public long FileSize { get; set; }
        public byte[]? FileData { get; set; }
    }
}
