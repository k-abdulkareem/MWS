using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MWS_SocialNetwork.ViewModels;

namespace MWS_SocialNetwork.Services
{
  public  interface IGroupService
    {
        MyGroupViewModel Get();
        List<GroupModel> GetMyGroups(string userId);
        Task<string> CreateGroup(CreateGroupModel model );
        List<GroupModel> FindGroup(string searchString);
        GroupModel GetGroupDetails(string gId);
        Task<bool> ChangeMembership(string gId, string value);
        Task<bool> SendInvitations(string gId, string[] ids);
        Task<bool> SetImage(string groupId , string fileName);
    }
}
