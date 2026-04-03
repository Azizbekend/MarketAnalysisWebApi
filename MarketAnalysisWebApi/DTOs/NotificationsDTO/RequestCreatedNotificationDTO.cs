namespace MarketAnalysisWebApi.DTOs.NotificationsDTO
{
    public class RequestCreatedNotificationDTO
    {
        public Guid RequestId { get; set; }
        public string RequestInnerId { get; set; }
        public string RequestName { get; set; }
        public string CustomerName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
