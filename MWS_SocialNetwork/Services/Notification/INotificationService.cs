using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MWS_SocialNetwork.ViewModels;

namespace MWS_SocialNetwork.Services
{
   public interface INotificationService
    {
        Task<bool> AddNotification(string fromUser , string toUser , string parameter , int code);

        Task<bool> AddManyNotification(string fromUser, List<string> toUsers, string parameter, int code);
        List<NotificationViewModel> GetNotifications(string toUser);

        Task<bool> SetNotificationAsSeen(List<int> notificationsId);
    }
}
