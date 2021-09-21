using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MWS_SocialNetwork.ViewModels
{
    public class u2pPrivacySettingModel
    {
        public int Id { get; set; } //U2PRelationshipId
        public List<SelectListItem> Relationships { get; set; }
        public List<SelectListItem> ExceptRelationshipsContacts { get; set; }
        public List<SelectListItem> Groups { get; set; }
        public List<SelectListItem> ExceptGroupsMembers { get; set; }
        public List<SelectListItem> Individuals { get; set; }

    }
}
