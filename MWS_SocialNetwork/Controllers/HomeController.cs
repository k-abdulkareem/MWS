using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MWS_SocialNetwork.Models;
using MWS_SocialNetwork.ViewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using MWS_SocialNetwork.Services;
using Microsoft.AspNetCore.Http;


namespace MWS_SocialNetwork.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IProfileService _profileService;
       

        public HomeController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager,IProfileService profileService )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _profileService=profileService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            var vm = new HomeViewModel() { SignInViewModel = null, SignUpViewModel = null };
           // ViewBag.operation  = "";
            return View(vm);
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp( SignUpViewModel model)
        {
           if (ModelState.IsValid)
           {
                  var userId =  await _profileService.AddUserEntity();
                    if(userId != null)
                    {
                        var user = new ApplicationUser
                        {
                            Id = userId,
                            UserName = model.Email,
                            Email = model.Email,
                            FullName = model.FirstName + " " + model.LastName,
                            ImageUrl = "/Profiles-Images/unknown.jpg"
                        };
                        var result = await _userManager.CreateAsync(user, model.Password);
                        if (result.Succeeded)
                        {
                            var FullNameClaim = new Claim("FullName", model.FirstName + " " + model.LastName);
                            await _userManager.AddClaimAsync(user, FullNameClaim);
                            var ImageUrlClaim = new Claim("ImageUrl", "/img/unknown.jpg");
                            await _userManager.AddClaimAsync(user, ImageUrlClaim);

                            await _profileService.IntializeUserProfile(user.Id);
                            await _signInManager.SignInAsync(user, isPersistent: false);
                                return Json("MyProfile/Index");
                        }
                        AddErrors(result);
                    }
                }
                return PartialView("_SignUpForm", model);
        }


        public async Task<IActionResult> Index(HomeViewModel model, string ReturnUrl)
        {
            ViewData["ReturnUrl"] = ReturnUrl;
            
                if (ModelState.IsValid)
                {
                    var result = await _signInManager.PasswordSignInAsync(model.SignInViewModel.Email, model.SignInViewModel.Password, false, false);
                    if (result.Succeeded)
                    {
                        if (ReturnUrl != null && ReturnUrl.Contains("ReturnUrl"))
                        {
                            var controller = "";
                            var action = "Index";
                            if (ReturnUrl.IndexOf("%", ReturnUrl.IndexOf("%") + 1) < 0)
                                controller = ReturnUrl.Substring(ReturnUrl.IndexOf("%")).Replace("%2F", "");
                            else
                            {
                                controller = ReturnUrl.Substring(ReturnUrl.IndexOf("%"), ReturnUrl.IndexOf("%", ReturnUrl.IndexOf("%") + 1) - ReturnUrl.IndexOf("%")).Replace("%2F", "");
                                action = ReturnUrl.Substring(ReturnUrl.IndexOf("%", ReturnUrl.IndexOf("%") + 1)).Replace("%2F", "");
                            }
                            return RedirectToAction(action, controller);

                        }
                        return RedirectToAction("Index", "MyPosts");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    }

                }

            return View(model);
        }



        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }



        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
