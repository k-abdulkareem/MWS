using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MWS_SocialNetwork.ViewModels
{
    public class UserProfileViewModel
    {
        public ProfileViewModel ProfileViewModel { get; set; }
        public string RelationshipId { get; set; }
        public List<RelationshipTypeViewModel> Relationships { get; set; }

        public AddPostAsContributorModel AddPostAsContributor { get; set; }

        public bool AllowShare { get; set; } = false;

        public List<PostDetailsModel> AllowedPosts { get; set; }
    }
}
