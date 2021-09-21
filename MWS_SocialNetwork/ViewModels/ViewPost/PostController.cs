using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MWS_SocialNetwork.ViewModels
{
    public class PostController
    {
       // public int PostId { get; set; }
        public string ControllerId { get; set; }
        public string ControllerName { get; set; }
        public string ControllerImageUrl { get; set; }
        public int U2PRelaionshipId { get; set; }
        public int Type { get; set;  }  //owner 1  - contributor 2 - Tagged 3  - shareHolder 4
         public u2pPrivacySettingModel PrivacySettings { get; set; }
        public bool IsMe { get; set; } = false;
    }
}
