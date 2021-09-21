using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using MWS_SocialNetwork.ViewModels;

namespace MWS_SocialNetwork.Services
{
  public  interface IAddPostService
    {
        UserProfileViewModel GetUserProfile(string contactId);

        AddPostAsOwnerModel GetPostForAddAsOwner();
        bool AddPostAsOwner(AddPostAsOwnerModel model);

        AddPostAsContributorModel GetPostForAddAsContributor(string contactId);
        bool AddPostAsContributor(AddPostAsContributorModel model);

        List<SelectListItem> WhoCanITag();

        bool AddPostAsShareHolder(int postId, string shareHolder);
    }
}
