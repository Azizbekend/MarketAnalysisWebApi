using MarketAnalysisWebApi.DbEntities;
using MarketAnalysisWebApi.DbEntities.Notifications;
using MarketAnalysisWebApi.DTOs.NotificationsDTO;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketAnalysisWebApi.NotificationServices
{
    public class NotificationService : INotificationService
    {
        private readonly AppDbContext _context;
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationService(
            AppDbContext context,
            IHubContext<NotificationHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        // Уведомление всех поставщиков о создании новой заявки
        public async Task NotifySuppliersAboutNewRequestAsync(Guid requestId)
        {
            var request = await _context.ProjectRequestsTable
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == requestId);

            if (request == null) return;

            // Получаем ID всех пользователей с ролью Supplier
            var supplierIds = await GetSupplierUserIdsAsync();

            if (!supplierIds.Any()) return;

            var notificationData = new RequestCreatedNotificationDTO

            {
                RequestId = request.Id,
                RequestInnerId = request.InnerId ?? request.Id.ToString().Substring(0, 8),
                RequestName = request.NameByProjectDocs,
                CustomerName = request.CustomerName,
                CreatedAt = request.CreatedAt
            };

            var notificationsToSave = new List<DbNotification>();

            // Создаем уведомление для каждого поставщика
            foreach (var supplierId in supplierIds)
            {
                var notification = new DbNotification
                {
                    Id = Guid.NewGuid(),
                    UserId = supplierId,
                    Title = "Новая заявка",
                    Message = $"Создана новая заявка: {request.NameByProjectDocs} от {request.CustomerName}",
                    Type = "RequestCreated",
                    DataJson = JsonConvert.SerializeObject(notificationData),
                    CreatedAt = DateTime.Now.ToUniversalTime().AddHours(3)
                };
                notificationsToSave.Add(notification);
            }

            await _context.NotificationsTable.AddRangeAsync(notificationsToSave);
            await _context.SaveChangesAsync();

            // Отправляем real-time уведомление через SignalR
            var signalRNotification = new NotificationDTO
            {
                Id = notificationsToSave.First().Id,
                Title = notificationsToSave.First().Title,
                Message = notificationsToSave.First().Message,
                Type = notificationsToSave.First().Type,
                CreatedAt = notificationsToSave.First().CreatedAt,
                Data = notificationData,
                IsRead = false
            };

            await NotificationHub.SendToAllSuppliers(_hubContext, signalRNotification);
        }

        // Уведомление заказчика о новом коммерческом предложении
        public async Task NotifyCustomerAboutNewOfferAsync(Guid offerId)
        {
            var offer = await _context.OffersTable
                .Include(o => o.Request)
                    .ThenInclude(r => r.User)
                .Include(o => o.BussinessAccount)
                    .ThenInclude(b => b.User)
                .FirstOrDefaultAsync(o => o.Id == offerId);

            if (offer?.Request?.User == null) return;

            var customerId = offer.Request.UserId;

            var notificationData = new OfferReceivedNotificationDTO
            {
                OfferId = offer.Id,
                RequestId = offer.RequestId,
                RequestName = offer.Request.NameByProjectDocs,
                SupplierName = offer.BussinessAccount?.User?.FullName ?? "Поставщик",
                PriceNDS = offer.CurrentPriceNDS,
                OffersNumber = offer.OffersNumber,
                ReceivedAt = DateTime.Now.ToUniversalTime().AddHours(3)
            };

            var notification = new DbNotification
            {
                Id = Guid.NewGuid(),
                UserId = customerId,
                Title = "Новое коммерческое предложение",
                Message = $"Поступило новое КП на заявку \"{offer.Request.NameByProjectDocs}\" от {notificationData.SupplierName}",
                Type = "OfferReceived",
                DataJson = JsonConvert.SerializeObject(notificationData),
                CreatedAt = DateTime.Now.ToUniversalTime().AddHours(3)
            };

            await _context.NotificationsTable.AddAsync(notification);
            await _context.SaveChangesAsync();

            // Отправляем real-time уведомление
            var signalRNotification = new NotificationDTO
            {
                Id = notification.Id,
                Title = notification.Title,
                Message = notification.Message,
                Type = notification.Type,
                CreatedAt = notification.CreatedAt,
                Data = notificationData,
                IsRead = false
            };

            await NotificationHub.SendToUser(_hubContext, customerId, signalRNotification);
        }

        // Получение уведомлений пользователя
        public async Task<List<NotificationDTO>> GetUserNotificationsAsync(Guid userId, bool onlyUnread = false)
        {
            var query = _context.NotificationsTable
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.CreatedAt);

            var notifications = onlyUnread
                ? await query.Where(n => !n.IsRead).ToListAsync()
                : await query.ToListAsync();

            return notifications.Select(n => new NotificationDTO
            {
                Id = n.Id,
                Title = n.Title,
                Message = n.Message,
                Type = n.Type,
                CreatedAt = n.CreatedAt,
                IsRead = n.IsRead,
                Data = !string.IsNullOrEmpty(n.DataJson)
                    ? JsonConvert.DeserializeObject<object>(n.DataJson)
                    : null
            }).ToList();
        }

        // Отметить уведомление как прочитанное
        public async Task<NotificationDTO> MarkAsReadAsync(Guid notificationId, Guid userId)
        {
            var notification = await _context.NotificationsTable
                .FirstOrDefaultAsync(n => n.Id == notificationId && n.UserId == userId);

            if (notification != null && !notification.IsRead)
            {
                notification.IsRead = true;
                notification.ReadAt = DateTime.Now.ToUniversalTime().AddHours(3);
                await _context.SaveChangesAsync();

                return new NotificationDTO
                {
                    Id = notification.Id,
                    Title = notification.Title,
                    Message = notification.Message,
                    Type = notification.Type,
                    CreatedAt = notification.CreatedAt,
                    IsRead = notification.IsRead,
                    Data = !string.IsNullOrEmpty(notification.DataJson)
                        ? JsonConvert.DeserializeObject<object>(notification.DataJson)
                        : null
                };
            }

            return null;
        }

        // Отметить все уведомления как прочитанные
        public async Task<int> MarkAllAsReadAsync(Guid userId)
        {
            var notifications = await _context.NotificationsTable
                .Where(n => n.UserId == userId && !n.IsRead)
                .ToListAsync();

            foreach (var notification in notifications)
            {
                notification.IsRead = true;
                notification.ReadAt = DateTime.Now.ToUniversalTime().AddHours(3);
            }

            await _context.SaveChangesAsync();
            return notifications.Count;
        }

        // Получить количество непрочитанных уведомлений
        public async Task<int> GetUnreadCountAsync(Guid userId)
        {
            return await _context.NotificationsTable
                .CountAsync(n => n.UserId == userId && !n.IsRead);
        }

        // Удалить уведомление
        public async Task<bool> DeleteNotificationAsync(Guid notificationId, Guid userId)
        {
            var notification = await _context.NotificationsTable
                .FirstOrDefaultAsync(n => n.Id == notificationId && n.UserId == userId);

            if (notification != null)
            {
                _context.NotificationsTable.Remove(notification);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        // Вспомогательный метод для получения ID всех поставщиков
        private async Task<List<Guid>> GetSupplierUserIdsAsync()
        {
            var supplierRole = await _context.UsersRolesTable
                .FirstOrDefaultAsync(r => r.RoleName == "Supplier");

            if (supplierRole == null) return new List<Guid>();

            return await _context.UsersTable
                .Where(u => u.RoleId == supplierRole.Id)
                .Select(u => u.Id)
                .ToListAsync();
        }

        // Вспомогательный метод для получения роли пользователя
        public async Task<string> GetUserRoleAsync(Guid userId)
        {
            var user = await _context.UsersTable
                .Include(u => u.UserRole)
                .FirstOrDefaultAsync(u => u.Id == userId);

            return user?.UserRole?.RoleName;
        }

    }
}
