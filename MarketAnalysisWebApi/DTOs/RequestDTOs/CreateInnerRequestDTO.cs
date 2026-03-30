using MarketAnalysisWebApi.DbEntities.DbEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketAnalysisWebApi.DTOs.RequestDTOs
{
    public class CreateInnerRequestDTO
    {
        public string? NameByProjectDocs { get; set; }
        public string? ObjectName { get; set; }
        public string? ObjectStage { get; set; }
        public string? ProjectDocsChapter { get; set; }
        public DateTime PublicationEndDate { get; set; }
        public string? CustomerName { get; set; }
        public string? ProjectOrganizationName { get; set; }
        public string? ContactName { get; set; }
        public string? PhoneNumber { get; set; }
        public Guid RegionId { get; set; }
        public Guid UserId { get; set; }
        public Guid ConfigTypeId { get; set; }
        public Guid FileId { get; set; }

    }
}
