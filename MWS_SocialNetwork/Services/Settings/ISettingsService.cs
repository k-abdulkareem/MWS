using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using MWS_SocialNetwork.ViewModels;

namespace MWS_SocialNetwork.Services
{
  public  interface ISettingsService
    {
        DefaultPrivacySettingsModel Get();
        DefaultPrivacySettingsModel GetDefaultPrivacySetting(string userId);
        u2pPrivacySettingModel GetPostPrivacySetting(int u2pRelationshipId);
        DefaultPrivacySettingsModel GetPermissions(string userId);

        bool SetDefaultPrivacySetting(DefaultPrivacySettingsModel model);
        bool SetPrivacySettingRow(u2pPrivacySettingModel model);
        bool SetPostPrivacySetting(u2pPrivacySettingModel model, string userId, int postId, int u2pType);
        bool SetPermissions(DefaultPrivacySettingsModel model);

        void DeletePrivacyRow(int u2pRelaionshipId);
        void DeletePermissionRow(int PermissionTypeId);

        List<SelectListItem> GetContactsOfRelationships(List<string> RelationshipIds);
        List<SelectListItem> GetGroupsMembers( List<string> groupsId);

    }
}
