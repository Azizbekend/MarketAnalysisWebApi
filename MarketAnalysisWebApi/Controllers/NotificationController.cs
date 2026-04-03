using MarketAnalysisWebApi.NotificationServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace MarketAnalysisWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationController(INotificationService notificationService, IHubContext<NotificationHub> hubContext)
        {
            _notificationService = notificationService;
            _hubContext = hubContext;
        }



        // Получить все уведомления пользователя
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserNotifications(Guid userId, [FromQuery] bool onlyUnread = false)
        {
            var notifications = await _notificationService.GetUserNotificationsAsync(userId, onlyUnread);
            return Ok(notifications);
        }

        // Получить количество непрочитанных уведомлений
        [HttpGet("user/{userId}/unread-count")]
        public async Task<IActionResult> GetUnreadCount(Guid userId)
        {
            var count = await _notificationService.GetUnreadCountAsync(userId);
            return Ok(new { userId, unreadCount = count });
        }

        // Отметить уведомление как прочитанное
        [HttpPut("user/{userId}/notification/{notificationId}/read")]
        public async Task<IActionResult> MarkAsRead(Guid userId, Guid notificationId)
        {
            var notification = await _notificationService.MarkAsReadAsync(notificationId, userId);
            if (notification == null)
                return NotFound(new { message = "Уведомление не найдено" });

            return Ok(notification);
        }

        // Отметить все уведомления как прочитанные
        [HttpPut("user/{userId}/mark-all-read")]
        public async Task<IActionResult> MarkAllAsRead(Guid userId)
        {
            var count = await _notificationService.MarkAllAsReadAsync(userId);
            return Ok(new { userId, markedCount = count });
        }

        // Удалить уведомление
        [HttpDelete("user/{userId}/notification/{notificationId}")]
        public async Task<IActionResult> DeleteNotification(Guid userId, Guid notificationId)
        {
            var result = await _notificationService.DeleteNotificationAsync(notificationId, userId);
            if (!result)
                return NotFound(new { message = "Уведомление не найдено" });

            return Ok(new { message = "Уведомление удалено" });
        }

        // Эндпоинт для регистрации подключения к SignalR
        [HttpPost("connect")]
        public async Task<IActionResult> ConnectToSignalR([FromBody] ConnectToSignalRRequest request)
        {
            // Этот эндпоинт используется клиентом для получения connectionId
            // Сам хаб обрабатывает подключение отдельно
            return Ok(new { message = "Подключение к SignalR будет установлено через WebSocket" });
        }
    }

    public class ConnectToSignalRRequest
    {
        public Guid UserId { get; set; }
        public string Role { get; set; }
    }
}

