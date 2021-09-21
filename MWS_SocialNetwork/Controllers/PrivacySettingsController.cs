using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MWS_SocialNetwork.ViewModels;
using Microsoft.AspNetCore.Mvc;
using MWS_SocialNetwork.Services;
using Microsoft.AspNetCore.Authorization;

namespace MWS_SocialNetwork.Controllers
{
    public class PrivacySettingsController : Controller
    {
        private readonly ISettingsService _settingsService;
        private readonly IContactService _contactService;

        public PrivacySettingsController(ISettingsService settingsService, IContactService contactService)
        {
            _settingsService = settingsService;
            _contactService = contactService;
        }

        [Authorize]
        public IActionResult Index()
        {
           var model = _settingsService.Get();
            return View(model);
        }

        [HttpPost]
        public JsonResult SavePrivacySettings( DefaultPrivacySettingsModel model)
        {
            var result = _settingsService.SetDefaultPrivacySetting(model);
           
            return Json(result);
        }

        [HttpPost]
        public JsonResult SavePermissions(DefaultPrivacySettingsModel model)
        {
            var result = _settingsService.SetPermissions(model);

            return Json(result);
        }


        [HttpGet]
        public JsonResult GetRelationshipsContacts(List<string> relationships)
        {
            if (relationships.Count > 0)
            {
                IEnumerable<System.Web.Mvc.SelectListItem> excepts = _settingsService.GetContactsOfRelationships(relationships);
                //var result = excepts.GroupBy(x => x.Group.Name);
                return Json(excepts);
            }
            return null;
        }


        [HttpGet]
        public JsonResult GetGroupsMembers(List<string> groups)
        {
            if (groups.Count > 0)
            {
                IEnumerable<System.Web.Mvc.SelectListItem> excepts = _settingsService.GetGroupsMembers(groups);
                return Json(excepts.ToArray());
            }
            return null;
        }


        [HttpGet]
        public JsonResult GetIndividuals(string term)
        {
            if (!string.IsNullOrEmpty(term))
            {
                IEnumerable<System.Web.Mvc.SelectListItem> users = _contactService.FindContact(term)
                    .Select(x => new System.Web.Mvc.SelectListItem {
                        Value = x.Id ,
                        Text = x.FullName ,
                    });
                return Json(users.ToArray());
            }
            return null;
        }
    }
}