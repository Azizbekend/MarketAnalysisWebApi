using MarketAnalysisWebApi.DbEntities.DbEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketAnalysisWebApi.DTOs.KnsCongigDTOs
{
    public class CreateKnsInnerRequestDTO
    {
        public string? NameByProjectDocs { get; set; }
        public string? ObjectName { get; set; }
        public string? LocationRegion { get; set; }
        public string? CustomerName { get; set; }
        public string? ProjectOrganizationName { get; set; }
        public string? ContactName { get; set; }
        public string? PhoneNumber { get; set; }
        public Guid UserId { get; set; }
        public Guid ConfigTypeId { get; set; }

    }
}
