using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MWS_SocialNetwork.ViewModels
{
    public class PostDetailsModel
    {
        public int? PostId { get; set; }
     
        public List<PostController> Controllers { get; set; }   
       
        public string PostDescription { get; set; }
        public string DisplayImageURL { get; set; }
        public string PostDateTime { get; set; }
        public string PostContent { get; set; }

        public List<string> LikesCount { get; set; }
        public List<string> SharesCount { get; set; }
    }
}
