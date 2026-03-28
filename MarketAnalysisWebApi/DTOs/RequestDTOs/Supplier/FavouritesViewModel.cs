using MarketAnalysisWebApi.DbEntities.DbEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketAnalysisWebApi.DTOs.RequestDTOs.Supplier
{
    public class FavouritesViewModel
    {
        public Guid Id { get; set; }
        public string? InnerId { get; set; }
        public string? ObjectName { get; set; }
        public string? LocationRegion { get; set; }
        public string? CustomerName { get; set; }
        public DateTime CreatedAt { get; set; }
        public RequestStatus Status { get; set; }
        public bool IsArchived { get; set; }
        public Guid UserId { get; set; }
        public Guid ConfigTypeId { get; set; }
        public bool IsFavorite { get; set; }
        public Guid? FavoriteId { get; set; }
    }
}
