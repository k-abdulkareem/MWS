using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MWS_SocialNetwork.ViewModels
{
    public class DefaultPrivacySettingsModel
    {
        public u2pPrivacySettingModel OwnerPrivacySettings { get; set; }
        public u2pPrivacySettingModel ContributorPrivacySettings { get; set; }
        public u2pPrivacySettingModel TaggedPrivacySettings { get; set; }
        public u2pPrivacySettingModel ShareHolderPrivacySettings { get; set; }
        public PermissionModel ContributingPermission { get; set; }
        public PermissionModel TaggingPermission { get; set; }
        public PermissionModel SharingPermission { get; set; }
    }
}
