using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MWS_SocialNetwork.ViewModels;
using MWS_SocialNetwork.Services;
using NToastNotify;
using Microsoft.AspNetCore.Authorization;

namespace MWS_SocialNetwork.Controllers
{
    public class AddPostController : Controller
    {
        private readonly IAddPostService _postService;
        private readonly ISettingsService _settingsService;
        private readonly IContactService _contactService;
        private readonly IToastNotification _toastNotification;

        public AddPostController(IAddPostService postService , ISettingsService settingsService ,IContactService contactService, IToastNotification toastNotification)
        {
            _postService = postService;
            _settingsService = settingsService;
            _contactService = contactService;
            _toastNotification = toastNotification;
        }

        [Authorize]
        public IActionResult Index()
        {
            var model = _postService.GetPostForAddAsOwner();
            return View(model);
        }

        [HttpPost]
        public JsonResult AddPost(AddPostAsOwnerModel model)
        {
            var result = _postService.AddPostAsOwner(model);
            return Json(result); 
        }

       
        
    }
}