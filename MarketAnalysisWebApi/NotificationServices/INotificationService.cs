using MarketAnalysisWebApi.DTOs.NotificationsDTO;

namespace MarketAnalysisWebApi.NotificationServices
{
    public interface INotificationService
    {
        Task NotifySuppliersAboutNewRequestAsync(Guid requestId);
        Task NotifyCustomerAboutNewOfferAsync(Guid offerId);
        Task<List<NotificationDTO>> GetUserNotificationsAsync(Guid userId, bool onlyUnread = false);
        Task<NotificationDTO> MarkAsReadAsync(Guid notificationId, Guid userId);
        Task<int> MarkAllAsReadAsync(Guid userId);
        Task<int> GetUnreadCountAsync(Guid userId);
        Task<bool> DeleteNotificationAsync(Guid notificationId, Guid userId);
    }
}
