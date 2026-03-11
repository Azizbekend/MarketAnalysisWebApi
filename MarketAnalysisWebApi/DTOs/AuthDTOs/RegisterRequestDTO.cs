using System.ComponentModel.DataAnnotations;

namespace MarketAnalysisWebApi.DTOs.AuthDTOs
{
    public class RegisterRequestDTO
    {
        [MaxLength(255)]
        public string? FullName { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [Phone]
        [MaxLength(25)]
        public string? PhoneNumber { get; set; }

        [Required]
        [MinLength(6)]
        public string? Password { get; set; }

        [Required]
        [Compare(nameof(Password))]
        public string? ConfirmPassword { get; set; }
        public string RoleName { get; set; }
    }
}
