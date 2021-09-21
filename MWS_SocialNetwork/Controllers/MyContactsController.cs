using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using MWS_SocialNetwork.Services;
using Microsoft.AspNetCore.Authorization;

namespace MWS_SocialNetwork.Controllers
{
    public class MyContactsController : Controller
    {
        private readonly IContactService _contactService; 

        public MyContactsController( IContactService contactService)
        {
            _contactService = contactService;
        }

        [Authorize]
        public IActionResult Index()
        {
            var model = _contactService.Get();
            return View(model);
        }

       
        public IActionResult FindContact(string searchString)
        {
            var model = _contactService.FindContact( searchString);
            return PartialView("_UsersList", model);
        }




    }
}