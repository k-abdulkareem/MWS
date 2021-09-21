using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MWS_SocialNetwork.ViewModels;
using MWS_SocialNetwork.Data;
using MWS_SocialNetwork.Settings;
using MWS_SocialNetwork.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace MWS_SocialNetwork.Services
{
    public class NotificationService : INotificationService
    {
        private readonly DatabaseContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string userId;

        public NotificationService(DatabaseContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;

        }

        public async Task<bool> AddNotification(string fromUser, string toUser, string parameter, int code)
        {
            var notification = new Notification
            {
                FromUserId = fromUser,
                ToUserId = toUser,
                Parameter = parameter,
                NotificationDate = DateTime.Now,
                NotificationTypeId = code
            };
            _context.Add(notification);
            var result = await _context.SaveChangesAsync();
            return result == 1;
        }


        public async Task<bool> AddManyNotification(string fromUser, List<string> toUsers, string parameter, int code)
        {
            List<Notification> notifications = new List<Notification>();
            if(toUsers != null)
            {
                foreach (var user in toUsers)
                {
                    var notification = new Notification
                    {
                        FromUserId = "3e62eef8-db55-4bd6-8315-f1bfecbb096e",
                        ToUserId = user,
                        Parameter = parameter,
                        NotificationDate = DateTime.Now,
                        NotificationTypeId = code
                    };
                    notifications.Add(notification);
                }
                _context.AddRange(notifications);
                var result = await _context.SaveChangesAsync();
                var x=1;
                return true;
            }
            return false;
            
        
        }

        public List<NotificationViewModel> GetNotifications(string toUser)
        {
            var notifications = _context.Set<Notification>().Where(x => x.ToUserId == toUser)
                .Include(x => x.FromUser).Include(x => x.NotificationType).OrderByDescending(x => x.Id)
                .Select(x => new NotificationViewModel {
                    Id = x.Id,
                    Action = x.NotificationType.Action,
                    Controller = x.NotificationType.Controller,
                    FromUserFullName = x.FromUser.FullName,
                    FromUserId = x.FromUserId,
                    Parameter = x.Parameter,
                    IsSeen = x.IsSeen,
                    Code = x.NotificationType.Code
                });

            return notifications.ToList();

        }

        public async  Task<bool> SetNotificationAsSeen(List<int> notificationsId)
        {
            var notifications = _context.Set<Notification>().Where(x => notificationsId.Contains(x.Id));
            foreach (var notification in notifications)
                notification.IsSeen = true;
            _context.UpdateRange(notifications);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

    }
}
