using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MWS_SocialNetwork.ViewModels;

namespace MWS_SocialNetwork.Services
{
 public   interface IViewPostService
    {
        PostDetailsModel GetPostDetails(int? postId);
        MyPostsModel GetPosts(string UserId);
        MyPostsModel GetMyPosts();
        List<PostDetailsModel> GetAllowedPosts(string requestUser, string targetUser);
        bool CheckVisibility(string targetUser, string requestUser, int? postId);
        int GetDecision(string user, int u2pId); // 0 disallow -- 1 allow
        Task<bool> AddInteraction(int postId, string user, int type);
    }
}
