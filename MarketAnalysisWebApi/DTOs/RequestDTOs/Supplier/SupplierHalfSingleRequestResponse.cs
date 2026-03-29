using MarketAnalysisWebApi.DbEntities.DbEntities;

namespace MarketAnalysisWebApi.DTOs.RequestDTOs.Supplier
{
    public class SupplierHalfSingleRequestResponse
    {
        public string? InnerId { get; set; }
        public string? LocationRegion { get; set; }
        public DateTime CreatedAt { get; set; }
        public RequestStatus Status { get; set; }
        public bool IsArchived { get; set; }
        public bool IsFavorite { get; set; }
        public Guid ConfigTypeId { get; set; }
        public Guid? SchemeFileId { get; set; }
        public string? SupplierRequestStatus { get; set; }
    }
}
