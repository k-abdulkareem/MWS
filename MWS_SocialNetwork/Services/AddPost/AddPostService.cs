using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MWS_SocialNetwork.Models;
using MWS_SocialNetwork.ViewModels;
using MWS_SocialNetwork.Data;
using Microsoft.AspNetCore.Http;
using System.Web.Mvc;
using MWS_SocialNetwork.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MWS_SocialNetwork.Services
{
    public class AddPostService :IAddPostService
    {

        private readonly DatabaseContext _context;
        private readonly ISettingsService _settingsService;
        private readonly IProfileService _profileService;
        private readonly ISharedService _sharedService;
        private readonly IContactService _contactService;
        private readonly INotificationService _notificationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string userId;

        public AddPostService(DatabaseContext context, ISettingsService settingsService, IContactService contactService , INotificationService notificationService,ISharedService sharedService, IProfileService profileService, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _settingsService = settingsService;
            _contactService = contactService;
            _sharedService = sharedService;
            _notificationService = notificationService;
            _httpContextAccessor = httpContextAccessor;
            _profileService = profileService;
        }

        public UserProfileViewModel GetUserProfile(string contactId)
        {
            var result = new UserProfileViewModel();

            result.ProfileViewModel = _profileService.GetProfile(contactId);
            result.RelationshipId = _profileService.GetRelationshipId(contactId);
            result.Relationships = _sharedService.GetRelationships();
            result.AddPostAsContributor = GetPostForAddAsContributor(contactId);
            result.AllowShare = CkeckPermission(contactId, PermissionTypeEnum.Sharing);
            return result;
        }


        public AddPostAsOwnerModel GetPostForAddAsOwner()
        {
            userId = UserManagerExtensions.GetCurrentUserId(_httpContextAccessor);

            var model = new AddPostAsOwnerModel();
            model.PrivacySettings = _settingsService.GetDefaultPrivacySetting(userId).OwnerPrivacySettings;
            model.TaggedUsers = WhoCanITag();
         
            return model;
        }

        public bool AddPostAsOwner(AddPostAsOwnerModel model)
        {
            try
            {
                userId = UserManagerExtensions.GetCurrentUserId(_httpContextAccessor);
                // add post content
                var post = new Post
                {
                    Content = model.Content,
                    PublishDate = DateTime.Now
                };
                _context.Add(post);
                int result = _context.SaveChanges();
                if (result == 1)
                {
                    //add owner privacy settings
                    var selectedTrueModel = SetU2PSelectedTrue(model.PrivacySettings);
                   
                    var k = _settingsService.SetPostPrivacySetting(selectedTrueModel, userId, post.Id, (int)U2PRelationshipTypeEnum.Owner);
                    //add tagged privacy settings
                    if (model.TaggedUsers != null)
                        foreach (var t in model.TaggedUsers)
                        {
                            var defaultModel = _settingsService.GetDefaultPrivacySetting(t.Value);
                            _settingsService.SetPostPrivacySetting(defaultModel.TaggedPrivacySettings, t.Value, post.Id, (int)U2PRelationshipTypeEnum.Tagged);
                            _notificationService.AddNotification(userId, t.Value, "Tagged", (int)NotificationCodesEnum.Tag);
                        }
                }
                return true;
            }
            catch(Exception ex)
            {
                Console.Write(ex.Message);
                return false;
            }
        }


        public AddPostAsContributorModel GetPostForAddAsContributor(string contactId)
        {
            userId = UserManagerExtensions.GetCurrentUserId(_httpContextAccessor);

            if (CkeckPermission(contactId, PermissionTypeEnum.Contributing))
            {
                var model = new AddPostAsContributorModel();
                model.OwnerId = contactId;
                model.ContributorPrivacySettings = _settingsService.GetDefaultPrivacySetting(userId).ContributorPrivacySettings;
                model.TaggedUsers = WhoCanITag();
                model.TaggedUsers.Remove(model.TaggedUsers.Find(x => x.Value == contactId));

                return model;
            }
            else
                return null;

        }

        public bool AddPostAsContributor(AddPostAsContributorModel model)
        {
            userId = UserManagerExtensions.GetCurrentUserId(_httpContextAccessor);
            // add post content
            var post = new Post
            {
                Content = model.Content,
                PublishDate = DateTime.Now
            };
            _context.Add(post);
            int result = _context.SaveChanges();
            if (result == 1)
            {
                //add owner privacy settings
                var OwnerdefaultModel = _settingsService.GetDefaultPrivacySetting(model.OwnerId);
                _settingsService.SetPostPrivacySetting(OwnerdefaultModel.OwnerPrivacySettings, model.OwnerId, post.Id, (int)U2PRelationshipTypeEnum.Owner);
                // add contributor privacy settings
                var selectedTrueModel = SetU2PSelectedTrue(model.ContributorPrivacySettings);
                _settingsService.SetPostPrivacySetting(selectedTrueModel, userId, post.Id, (int)U2PRelationshipTypeEnum.Contributor);
                _notificationService.AddNotification(userId, model.OwnerId, "Owned", (int)NotificationCodesEnum.Contribute);
                //add tagged privacy settings
                if (model.TaggedUsers != null)
                    foreach (var t in model.TaggedUsers)
                    {
                        var defaultModel = _settingsService.GetDefaultPrivacySetting(t.Value);
                        _settingsService.SetPostPrivacySetting(defaultModel.TaggedPrivacySettings, t.Value, post.Id, (int)U2PRelationshipTypeEnum.Tagged);
                        _notificationService.AddNotification(userId, t.Value, "Tagged", (int)NotificationCodesEnum.Tag);
                    }
            }
            return true;
        }



        public List<SelectListItem> WhoCanITag()
        {
            userId = UserManagerExtensions.GetCurrentUserId(_httpContextAccessor);

            var TaggedList = new List<SelectListItem>();
            var people = _context.Set<U2URelationship>().Where(x => x.ContactId == userId).Include(x => x.User);
           
            foreach(var p in people)
            {
                var permissions = _context.Set<Permission>().Where(x => x.UserId == p.UserId && x.PermissionTypeId == (int)PermissionTypeEnum.Tagging);
                var disallowed = _context.Set<DisallowedPermission>().Where(x => permissions.Select(y => y.Id).Contains(x.PermissionId));
                if (disallowed.Select(x => x.DisallowedUserId).Contains(userId))
                    break;
                if (permissions.Select(x => x.RelationshipTypeId).Contains(p.RelationshipTypeId))
                    TaggedList.Add(new SelectListItem {
                        Text = p.User.FullName,
                        Value = p.UserId
                    });
            }
            return TaggedList;
        }


        public bool CkeckPermission(string contactId,PermissionTypeEnum pType)
        {
            userId = UserManagerExtensions.GetCurrentUserId(_httpContextAccessor);

            var u2uRelationship = _context.Set<U2URelationship>().Where(x => x.ContactId == userId && x.UserId == contactId).FirstOrDefault();
             
            if (u2uRelationship != null)
            {
                var permissions = _context.Set<Permission>().Where(x => x.UserId == contactId && x.PermissionTypeId == (int)pType);
                var disallowed = _context.Set<DisallowedPermission>().Where(x => permissions.Select(y => y.Id).Contains(x.PermissionId));
                if (disallowed.Select(x => x.DisallowedUserId).Contains(userId))
                    return false;
                if (permissions.Select(x => x.RelationshipTypeId).Contains(u2uRelationship.RelationshipTypeId))
                    return true;
            }
            return false;
        }


        public u2pPrivacySettingModel SetU2PSelectedTrue(u2pPrivacySettingModel model)
        {
            if (model.Relationships != null)
                foreach (var item in model.Relationships)
                    item.Selected = true;
            if (model.ExceptRelationshipsContacts  != null)
                foreach (var item in model.ExceptRelationshipsContacts)
                    item.Selected = true;
            if (model.Groups != null)
                foreach (var item in model.Groups)
                    item.Selected = true;
            if (model.ExceptGroupsMembers != null)
                foreach (var item in model.ExceptGroupsMembers)
                    item.Selected = true;
            if (model.Individuals != null)
                foreach (var item in model.Individuals)
                    item.Selected = true;

            return model;
         }


        public bool AddPostAsShareHolder(int postId, string shareHolder)
        {
            var privacy = _settingsService.GetDefaultPrivacySetting(shareHolder).ShareHolderPrivacySettings;
            _settingsService.SetPostPrivacySetting(privacy, shareHolder, postId, (int)U2PRelationshipTypeEnum.ShareHolder);
            return true;
        }

    }
}
