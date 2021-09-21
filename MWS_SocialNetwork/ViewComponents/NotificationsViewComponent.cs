using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MWS_SocialNetwork.Services;
namespace MWS_SocialNetwork.ViewComponents
{

    public class NotificationsViewComponent:ViewComponent
    {
        private readonly INotificationService _notificationService;
        private readonly IProfileService _profileService;

        public NotificationsViewComponent(INotificationService notificationService, IProfileService profileService)
        {
            _notificationService = notificationService;
            _profileService = profileService;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId = _profileService.GetUserId();
            var model = _notificationService.GetNotifications(userId);
            return View(model);
        }

    }
}
