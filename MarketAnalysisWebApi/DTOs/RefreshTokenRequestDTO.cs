using System.ComponentModel.DataAnnotations;

namespace MarketAnalysisWebApi.DTOs
{
    public class RefreshTokenRequestDTO
    {
        [Required]
        public string? RefreshToken { get; set; }
    }
}
