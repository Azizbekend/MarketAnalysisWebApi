using System.ComponentModel.DataAnnotations;

namespace MarketAnalysisWebApi.DTOs.AuthDTOs
{
    public class RefreshTokenRequestDTO
    {
        [Required]
        public string? RefreshToken { get; set; }
    }
}
