using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MWS_SocialNetwork.ViewModels
{
    public class MyGroupViewModel
    {
        public List<GroupModel> MyGroups { get; set; }
        public int MyGroupsCount { get; set; }
        public CreateGroupModel CreateGroup { get; set; }
    }
}
