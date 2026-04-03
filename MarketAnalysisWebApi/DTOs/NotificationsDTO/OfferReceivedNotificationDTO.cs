namespace MarketAnalysisWebApi.DTOs.NotificationsDTO
{
    public class OfferReceivedNotificationDTO
    {
        public Guid OfferId { get; set; }
        public Guid RequestId { get; set; }
        public string RequestName { get; set; }
        public string SupplierName { get; set; }
        public double PriceNDS { get; set; }
        public string OffersNumber { get; set; }
        public DateTime ReceivedAt { get; set; }
    }
}
