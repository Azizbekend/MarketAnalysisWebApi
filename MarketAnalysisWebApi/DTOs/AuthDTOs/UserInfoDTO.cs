namespace MarketAnalysisWebApi.DTOs.AuthDTOs
{
    public class UserInfoDTO
    {
        public Guid Id { get; set; }
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Role { get; set; }
    }
}
