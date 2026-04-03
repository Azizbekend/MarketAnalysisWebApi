//using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MarketAnalysisWebApi.NotificationServices
{
    //[Authorize]
    public class NotificationHub : Hub
    {
        // Хранилище connectionId для каждого пользователя
        private static readonly ConcurrentDictionary<Guid, HashSet<string>> _userConnections = new();

        public async Task RegisterUser(Guid userId)
        {
            // Добавляем connectionId в список подключений пользователя
            _userConnections.AddOrUpdate(
                userId,
                new HashSet<string> { Context.ConnectionId },
                (key, existingSet) =>
                {
                    existingSet.Add(Context.ConnectionId);
                    return existingSet;
                }
            );

            // Добавляем connection в группу пользователя
            await Groups.AddToGroupAsync(Context.ConnectionId, $"user_{userId}");

            // Проверяем роль пользователя через отдельный сервис
            // Роль нужно передать при регистрации
        }

        public async Task RegisterSupplier(Guid userId)
        {
            await RegisterUser(userId);
            await Groups.AddToGroupAsync(Context.ConnectionId, "suppliers");
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            // Удаляем connectionId пользователя
            foreach (var kvp in _userConnections)
            {
                if (kvp.Value.Remove(Context.ConnectionId))
                {
                    if (kvp.Value.Count == 0)
                    {
                        _userConnections.TryRemove(kvp.Key, out _);
                    }
                    break;
                }
            }

            await base.OnDisconnectedAsync(exception);
        }

        // Отправка уведомления конкретному пользователю
        public static async Task SendToUser(IHubContext<NotificationHub> hubContext, Guid userId, object notification)
        {
            await hubContext.Clients.Group($"user_{userId}").SendAsync("ReceiveNotification", notification);
        }

        // Отправка уведомления всем поставщикам
        public static async Task SendToAllSuppliers(IHubContext<NotificationHub> hubContext, object notification)
        {
            await hubContext.Clients.Group("suppliers").SendAsync("ReceiveNotification", notification);
        }

        // Проверка, онлайн ли пользователь
        public static bool IsUserOnline(Guid userId)
        {
            return _userConnections.ContainsKey(userId);
        }
    }
}
