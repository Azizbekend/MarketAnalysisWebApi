using MarketAnalysisWebApi.DbEntities.Base;
using Org.BouncyCastle.Asn1.Ocsp;

namespace MarketAnalysisWebApi.DbEntities.Notifications
{
    public class DbNotification : DbBaseEntity
    {
        public string Title { get; set; }   // Заголовок: "Новая заявка #123"

        public string Message { get; set; } // Текст: "Клиент создал новую заявку..."

        public string Type { get; set; }    // Тип: "info", "success", "warning", "error"

        public DateTime CreatedAt { get; set; } // Когда создано

        /// <summary>
        /// ID связанной заявки (если уведомление о заявке)
        /// </summary>
        public int? RequestId { get; set; }

        /// <summary>
        /// Сама заявка (для связи)
        /// </summary>
        public Request Request { get; set; }

        /// <summary>
        /// Глобальное ли уведомление? (видят все или только некоторые)
        /// </summary>
        public bool IsGlobal { get; set; }
    }
}
