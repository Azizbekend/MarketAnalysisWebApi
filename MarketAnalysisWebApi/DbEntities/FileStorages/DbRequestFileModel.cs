using MarketAnalysisWebApi.DbEntities.Base;
using MarketAnalysisWebApi.DbEntities.DbEntities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MarketAnalysisWebApi.DbEntities.FileStorages
{
    public class DbRequestFileModel 
    {
        [Key]
        public Guid ? Id { get; set; }
        public string? FileName { get; set; }
        public string? ContentType { get; set; }
        public long FileSize { get; set; }
        public byte[]? FileData { get; set; }

        [JsonIgnore]
        public ICollection<DbProjectRequest> ? Requests { get; set; }
        
    }
}
