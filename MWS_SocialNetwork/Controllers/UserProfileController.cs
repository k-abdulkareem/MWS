using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MWS_SocialNetwork.Services;
using Microsoft.AspNetCore.Identity;
using NToastNotify;
using MWS_SocialNetwork.ViewModels;
using MWS_SocialNetwork.Settings;
using Microsoft.AspNetCore.Authorization;

namespace MWS_SocialNetwork.Controllers
{
    public class UserProfileController : Controller
    {
        private readonly IAddPostService _postService;
        private readonly IViewPostService _viewPostService;
        private readonly IProfileService _profileService;
        private readonly IToastNotification _toastNotification;


        public UserProfileController( IAddPostService postService, IProfileService profileService, IViewPostService viewPostService, IToastNotification toastNotification)
        {
            _postService = postService;
            _profileService = profileService;
            _viewPostService = viewPostService;
            _toastNotification = toastNotification;
        }

        [Authorize]
        public IActionResult Index(string cId)
        {
            var userId = _profileService.GetUserId();
            ViewData["currentUserId"] = userId;
            if (userId == cId)
             return   RedirectToAction("Index", "MyProfile");
            
            var model = _postService.GetUserProfile(cId);
            model.AllowedPosts = _viewPostService.GetAllowedPosts(userId, cId);
            if (model.AllowedPosts != null)
            {
                if(model.ProfileViewModel.PersonalViewModel.Gender == "Male")
                foreach (var post in model.AllowedPosts)
                {
                    post.PostDescription = post.PostDescription.Replace("with you", "with him");
                    post.PostDescription = post.PostDescription.Replace("to your", "to his");
                    post.PostDescription = post.PostDescription.Replace("Tagged you", "Tagged him");
                        post.PostDescription = post.PostDescription.Replace("You", "He");
                        post.PostDescription = post.PostDescription.Replace("you", "he");
                    }
                else
                    foreach (var post in model.AllowedPosts)
                    {
                        post.PostDescription = post.PostDescription.Replace("with you", "with her");
                        post.PostDescription = post.PostDescription.Replace("to your", "to her");
                        post.PostDescription = post.PostDescription.Replace("Tagged you", "Tagged her");
                        post.PostDescription = post.PostDescription.Replace("You", "She");
                        post.PostDescription = post.PostDescription.Replace("you", "she");
                    }

            }
            return View(model);
            
        }

        [HttpPost]
        public async Task<IActionResult> Index(string cId , string rId)
        {
              var result = await _profileService.ChangeRelationship( cId, rId);
            
            var model = _profileService.GetUserProfile( cId);
            //System.Threading.Thread.Sleep(2000);
            return PartialView("_ChangeRelationshipView",model);
        }



        [HttpPost]
        public JsonResult AddPost(AddPostAsContributorModel model)
        {
            var result = _postService.AddPostAsContributor(model);
            return Json(result);
        }

        [HttpPost]
        public JsonResult SetLike(string postId)
        {
          var userId = _profileService.GetUserId();
            var result = _viewPostService.AddInteraction(Convert.ToInt32(postId), userId, (int)InteractionTypeEnum.Like);
            return Json(result.Result);
        }

        [HttpPost]
        public JsonResult SharePost(string postId)
        {
            var userId = _profileService.GetUserId();
            _postService.AddPostAsShareHolder(Convert.ToInt32(postId), userId);
            var result = _viewPostService.AddInteraction(Convert.ToInt32(postId), userId, (int)InteractionTypeEnum.Share);
            return Json(result.Result);
        }

    }
}