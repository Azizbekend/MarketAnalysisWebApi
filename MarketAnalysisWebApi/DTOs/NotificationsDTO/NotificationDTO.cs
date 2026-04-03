namespace MarketAnalysisWebApi.DTOs.NotificationsDTO
{
    public class NotificationDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Type { get; set; } // "RequestCreated", "OfferReceived"
        public DateTime CreatedAt { get; set; }
        public object Data { get; set; }
        public bool IsRead { get; set; }
    }
}
