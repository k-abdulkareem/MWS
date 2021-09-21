using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MWS_SocialNetwork.ViewModels
{
    public class GroupModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public bool IsMember { get; set; } 
        public int MembersCount { get; set; }
        public int PostsCount { get; set; }
        public DateTime CreationDate { get; set; }
        public List<ContactModel> Members { get; set; }
        
    }
}
