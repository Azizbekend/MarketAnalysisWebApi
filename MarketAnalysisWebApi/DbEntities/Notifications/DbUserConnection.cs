using MarketAnalysisWebApi.DbEntities.Base;

namespace MarketAnalysisWebApi.DbEntities.Notifications
{
    public class DbUserConnection : DbBaseEntity
    {
        /// <summary>
        /// ID пользователя (кто подключился)
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Уникальный ID подключения, который даёт SignalR
        /// Через него мы можем отправить сообщение конкретному браузеру
        /// </summary>
        public string ConnectionId { get; set; }

        /// <summary>
        /// Когда пользователь подключился
        /// </summary>
        public DateTime ConnectedAt { get; set; }

        /// <summary>
        /// Активно ли подключение сейчас?
        /// Если пользователь закрыл браузер - ставим false
        /// </summary>
        public bool IsActive { get; set; }
    }
}
