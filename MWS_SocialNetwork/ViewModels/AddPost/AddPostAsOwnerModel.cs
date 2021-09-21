using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MWS_SocialNetwork.ViewModels
{
    public class AddPostAsOwnerModel
    {
        public string Content { get; set; }
        public List<SelectListItem>  TaggedUsers { get; set; }
        public u2pPrivacySettingModel PrivacySettings { get; set; }
    }
}
