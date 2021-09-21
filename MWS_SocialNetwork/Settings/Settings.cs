using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MWS_SocialNetwork.Settings
{

    public enum InteractionTypeEnum
    {
        Like = 0,
        Share = 1
    }

    public enum EntityTypeEnum
    {
        Relationship = 3,
        Group = 2,
        User = 1
    }

    public enum U2PRelationshipTypeEnum
    {
        Owner = 1,
        Contributor = 2,
        Tagged = 3,
        ShareHolder = 4
    }

    public enum PermissionTypeEnum
    {
        Contributing = 1,
        Tagging = 2,
        Sharing = 3
    }

    public enum ConflictResolutionPoliciesEnum
    {
          All = 1,
          One = 2,
          Majority = 3
    }

    public enum NotificationCodesEnum
    {
        AddRelationship = 1,
        ChangeConflictPolicy = 2,
        ChangeRelationship = 3 ,
        GroupInvitation = 4,
        Tag = 5,
        Contribute = 6
    }

}
