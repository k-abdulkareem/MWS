using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MWS_SocialNetwork.Services;
using MWS_SocialNetwork.ViewModels;
using NToastNotify;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace MWS_SocialNetwork.Controllers
{
    public class MyProfileController : Controller
    {
        private readonly INotificationService _notificationService;
        private readonly IProfileService _profileService;
        private readonly IToastNotification _toastNotification;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public MyProfileController(INotificationService notificationService, IProfileService profileService , IToastNotification toastNotification , IHostingEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _notificationService = notificationService;
            _profileService = profileService;
            _toastNotification = toastNotification;
            this.hostingEnvironment = hostingEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }

        [Authorize]
        public IActionResult Index()
        {
            var model = _profileService.GetMyProfile(); 
            model.ActiveTab ="Personal" ;
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Index(ProfileViewModel model, string section)
        {
            var activeTab = "Personal";
            if (section == "Personal")
            {
                var result = await _profileService.UpdatePersonal(model.PersonalViewModel); 
                if(result)
                    _toastNotification.AddSuccessToastMessage("Successfully Updated ..");
                else
                    _toastNotification.AddErrorToastMessage("Failed Update ..");
            }
           if(section == "Work")
            {
                activeTab = "Work";
                var result = await _profileService.UpdateWork(model.WorkViewModel);
                if (result)
                    _toastNotification.AddSuccessToastMessage("Successfully Updated ..");
                else

                    _toastNotification.AddErrorToastMessage("Failed Update ..");
            }
           if(section == "Eduation")
            {
                activeTab = "Education";
                var result= await _profileService.UpdateEducation(model.EducationViewModel);
                if (result)
                    _toastNotification.AddSuccessToastMessage("Successfully Updated ..");
                else

                    _toastNotification.AddErrorToastMessage("Failed Update ..");
            }
            if (section == "Image")
            {
                var userId = UserManagerExtensions.GetCurrentUserId(_httpContextAccessor);
                string uniqueFileName = null;
                if (model.Image != null)
                {
                    // The image must be uploaded to the images folder in wwwroot
                    // To get the path of the wwwroot folder we are using the inject
                    // HostingEnvironment service provided by ASP.NET Core
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "Profiles-Images");
                    // To make sure the file name is unique we are appending a new
                    // GUID value and and an underscore to the file name
                    //int index= model.Image.FileName.IndexOf('.');
                    uniqueFileName = userId + ".jpg";//model.Image.FileName.Substring(index);
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    // Use CopyTo() method provided by IFormFile interface to
                    // copy the file to wwwroot/images folder
                    model.Image.CopyTo(new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite));
                    await _profileService.ChangeImage(uniqueFileName,userId);
                }
             
            }
            //end
            var vm = _profileService.GetMyProfile();
            vm.ActiveTab = activeTab;
            return View(vm);
        }


        public ActionResult GetNotifications()
        {
            var userId = _profileService.GetUserId();
            var model = _notificationService.GetNotifications(userId);
            return View("_NotificationsPartialView", model);
        }
    }
}