using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MWS_SocialNetwork.ViewModels
{
    public class MyPostsModel
    {
        public List<PostDetailsModel> AllPosts { get; set; }
        public List<PostDetailsModel> AsOwner { get; set; }
        public List<PostDetailsModel> AsContributor { get; set; }
        public List<PostDetailsModel> AsTagged { get; set; }
        public List<PostDetailsModel> AsShareHolder { get; set; }

    }
}
