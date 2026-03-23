namespace MarketAnalysisWebApi.DTOs.UserDTOs
{
    public class MailConfirmCodeDTO
    {
        public Guid ConfirmSessionId { get; set; }
        public string? Code { get; set; }
    }
}
