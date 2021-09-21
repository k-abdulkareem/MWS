using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MWS_SocialNetwork.Services;
using MWS_SocialNetwork.ViewModels;
using NToastNotify;

namespace MWS_SocialNetwork.Controllers
{
    public class MyGroupsController : Controller
    {
        private readonly IGroupService _groupService;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IToastNotification _toastNotification;

        public MyGroupsController(IGroupService groupService, IHostingEnvironment hostingEnvironment, IToastNotification toastNotification)
        {
            _groupService = groupService;
            this.hostingEnvironment = hostingEnvironment;
            _toastNotification = toastNotification;
        }


        [HttpGet]
        [Authorize]
        public IActionResult Index(int ActiveTab = 0)
        {
            if (ActiveTab == 1)
                ViewData["ActiveTab"] = "CreateGroup";
            else
                ViewData["ActiveTab"] = "AllGroups";
            var model = _groupService.Get();
            return View(model);
        }



        public IActionResult FindGroups(string searchString)
         {   
            var model = _groupService.FindGroup(searchString);
            return PartialView("_SearchView", model);
        }



        [HttpPost]
        public async Task<IActionResult> Create(CreateGroupModel model)
        {
            if (model.Image == null)
            {
                _toastNotification.AddWarningToastMessage("Please Choose a Photo to Your Group");
                return RedirectToAction("Index", new { ActiveTab = 1 });
            }
            if(String.IsNullOrEmpty(model.Name.Trim()))
            {
                _toastNotification.AddWarningToastMessage("Please Choose a Name to Your Group");
                return RedirectToAction("Index", new { ActiveTab = 1 });
            }
            
           
            var newId = await  _groupService.CreateGroup(model);
            if(newId != null)
            {
                 string   uniqueFileName = newId + ".jpg";
                
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "Groups-Images");
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                model.Image.CopyTo(new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite));
                await _groupService.SetImage(newId, uniqueFileName);

                //send invitations
                if (model.SelectedValues != null)
                    await _groupService.SendInvitations(newId, model.SelectedValues);

                _toastNotification.AddSuccessToastMessage("Successfully Added Group & Invitations sent");
            }
            else
                _toastNotification.AddErrorToastMessage("Something Went Wrong ");

            return RedirectToAction("Index");

        }
    }
}