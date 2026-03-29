using MarketAnalysisWebApi.DbEntities.DbEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketAnalysisWebApi.DTOs.RequestDTOs.Supplier
{
    public class SupplierSingleRequestResponse
    {
        public string? InnerId { get; set; }
        public string? NameByProjectDocs { get; set; }
        public string? ObjectName { get; set; }
        public string? LocationRegion { get; set; }
        public string? CustomerName { get; set; }
        public string? ProjectOrganizationName { get; set; }
        public string? ContactName { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; } 
        public RequestStatus Status { get; set; } 
        public bool IsArchived { get; set; }
        public bool IsFavorite { get; set; }
        public Guid UserId { get; set; }
        public Guid ConfigTypeId { get; set; }
        public Guid ? SchemeFileId { get; set; }
        public string? SupplierRequestStatus { get; set; }
    }
}
