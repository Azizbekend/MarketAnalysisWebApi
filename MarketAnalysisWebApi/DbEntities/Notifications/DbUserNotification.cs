using MarketAnalysisWebApi.DbEntities.Base;

namespace MarketAnalysisWebApi.DbEntities.Notifications
{
    public class DbUserNotification : DbBaseEntity
    {
        /// <summary>
        /// Какой пользователь получил уведомление
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Какое уведомление он получил
        /// </summary>
        public int NotificationId { get; set; }

        /// <summary>
        /// Само уведомление (для связи)
        /// </summary>
        public DbNotification Notification { get; set; }

        /// <summary>
        /// Прочитал ли пользователь это уведомление?
        /// true - прочитал, false - ещё нет
        /// </summary>
        public bool IsRead { get; set; }

        /// <summary>
        /// Когда прочитал (если прочитал)
        /// </summary>
        public DateTime? ReadAt { get; set; }

        /// <summary>
        /// Было ли доставлено через SignalR?
        /// true - отправили в браузер, false - сохранили для офлайн-доставки
        /// </summary>
        public bool IsDelivered { get; set; }

        /// <summary>
        /// Когда доставили
        /// </summary>
        public DateTime? DeliveredAt { get; set; }

        /// <summary>
        /// В архиве ли уведомление? (пользователь скрыл)
        /// </summary>
        public bool IsArchived { get; set; }

        /// <summary>
        /// Когда отправили в архив
        /// </summary>
        public DateTime? ArchivedAt { get; set; }
    }
}
