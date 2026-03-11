namespace MarketAnalysisWebApi.DTOs
{
    public class LoginRequestDTO
    {
        public required string Email { get; init; }
        public required string Password { get; init; }
    }
}
