using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MWS_SocialNetwork.Services;
using MWS_SocialNetwork.ViewModels;

namespace MWS_SocialNetwork.Controllers
{
   
    public class GroupProfileController : Controller
    {

        private readonly IGroupService _groupService;

        public GroupProfileController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Index(string gId)
        {
            var model = _groupService.GetGroupDetails(gId); 
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(GroupModel model , string Membership)
        {
            var result = await _groupService.ChangeMembership(model.Id, Membership);
            var vm = _groupService.GetGroupDetails(model.Id);
            return View(vm);
        }
    }
}