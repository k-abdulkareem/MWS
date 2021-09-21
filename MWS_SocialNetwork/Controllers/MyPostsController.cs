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
    public class MyPostsController : Controller
    {
        private readonly IViewPostService _postService;
        private readonly ISettingsService _settingsService;
        private readonly IContactService _contactService;

        public MyPostsController(IViewPostService postService, ISettingsService settingsService, IContactService contactService)
        {
            _postService = postService;
            _settingsService = settingsService;
            _contactService = contactService;
        }

        [Authorize]
        public IActionResult Index( string active = "AllPosts")
        {
            ViewData["active"] = active;
            var model = _postService.GetMyPosts();
            return View(model);
        }


        public IActionResult ShowPrivacySettings(string postId)
        {
            var model = _postService.GetMyPosts().AllPosts.Where(x => x.PostId == Convert.ToInt32(postId)).FirstOrDefault();
          //  var model = _postService.GetPostDetails(Convert.ToInt32(postId));
            return PartialView("_PrivacySettingsPartialView",model.Controllers.Where(x => x.IsMe).FirstOrDefault().PrivacySettings);
        }


        public IActionResult ShowVisibility(string postId)
        {
            return PartialView("_CheckVisibilityPartialView");
        }

        [HttpPost]
        public JsonResult ChangePrivacySettings(u2pPrivacySettingModel model)
        {
            var result = _settingsService.SetPrivacySettingRow(model);
            return Json(result);
        }


        [HttpPost]
        public IActionResult CheckVisibility(string userId , string postId )
        {
            var result = _postService.CheckVisibility(null,userId, Convert.ToInt32(postId));
            if (result)
                return PartialView("_AllowedPartialView");
            else
                return PartialView("_DeniedPartialView");
        }



    }
}