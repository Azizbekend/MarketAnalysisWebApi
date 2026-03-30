using MarketAnalysisWebApi.DbEntities.DbEntities;
using MarketAnalysisWebApi.DbEntities.FileStorages;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketAnalysisWebApi.DTOs.RequestDTOs.Supplier
{
    public class JoinSupplierRequestTableDTO
    {
        public Guid RequestId { get; set; }
        public string? InnerId { get; set; }
        public string? NameByProjectDocs { get; set; }
        public string? ObjectName { get; set; }
        public string? ObjectStage { get; set; }
        public string? ProjectDocsChapter { get; set; }
        public DateTime PublicationEndDate { get; set; }
        public string? CustomerName { get; set; }
        public string? ProjectOrganizationName { get; set; }
        public string? ContactName { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; } 
        public RequestStatus Status { get; set; }
        public string ? ViewPayStatus { get; set; }
        public int BusinessOffersCount { get; set; }
        public Guid? RegionId { get; set; }
        public DbRegion? Region { get; set; }
        public bool IsArchived { get; set; } = false;
        public Guid ConfigTypeId { get; set; }
        public DbRequestConfigType? RequestConfigType { get; set; }
        public JoinKnsConfigDTO ? KnsConfigDTO { get; set; }
        public JoinLosConfigDTO ? LosConfigDTO { get; set; }
        public JoinPumpConfigDTO ? PumpConfigDTO { get; set; }
    }
}
