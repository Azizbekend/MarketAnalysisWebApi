namespace MarketAnalysisWebApi.DTOs.AuthDTOs
{
    public class AuthResponseDTO
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime ExpiresAt { get; set; }
        public UserInfoDTO? User { get; set; }
    }
}
