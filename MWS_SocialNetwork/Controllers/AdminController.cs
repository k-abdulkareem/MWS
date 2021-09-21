using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MWS_SocialNetwork.ViewModels;
using MWS_SocialNetwork.Services;
using NToastNotify;
using Microsoft.Extensions.Configuration;

namespace MWS_SocialNetwork.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IToastNotification _toastNotification;
        private readonly IConfiguration _configuration;
        public AdminController(IAdminService adminService , IToastNotification toastNotification , IConfiguration configuration)
        {
            _adminService = adminService;
            _toastNotification = toastNotification;
            _configuration = configuration;
        }


        public IActionResult Index()
        {
            TempData["login"] = "false";
            var model = new AdminLogin();
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(AdminLogin model)
        {
            var u = _configuration.GetSection("AdminLogin:UserName").Value;
            var p = _configuration.GetSection("AdminLogin:Password").Value;
            if(model.UserName == u && model.Password == p)
            {
                TempData["login"] = "true";
                return RedirectToAction("Control");
            }
            else
            {
                TempData["login"] = "false";
                _toastNotification.AddErrorToastMessage("Falied Login");
                return RedirectToAction("Index");
            }

            
        }


        public IActionResult Control()
        {
            if (TempData["login"] == null )
                return RedirectToAction("Index");
            if(TempData["login"].ToString() != "true")
                return RedirectToAction("Index");
            
                var model = _adminService.Get();
                return View(model);
        }

        [HttpPost]
        public IActionResult AddRelationship(string title)
        {
            TempData["login"] = "true";
            var result = _adminService.AddRelationship(title);
            if (result)
                _toastNotification.AddSuccessToastMessage("Successful");
            else
                _toastNotification.AddErrorToastMessage("Something Went Wrong");
            return RedirectToAction("Control");
        }

        [HttpPost]
        public IActionResult ChangePolicy(string policy)
        {
            TempData["login"] = "true";
            var result = _adminService.ChangePolicy(policy);
            if (result)
                _toastNotification.AddSuccessToastMessage("Successful");
            else
                _toastNotification.AddErrorToastMessage("Something Went Wrong");
            return RedirectToAction("Control");
        }



    }
}