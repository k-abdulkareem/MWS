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
    public class ViewPostService:IViewPostService
    {
        private readonly DatabaseContext _context;
        private readonly ISettingsService _settingsService;
        private readonly IProfileService _profileService;
        private readonly IGroupService _groupService;
        private readonly ISharedService _sharedService;
        private readonly IContactService _contactService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string userId;

        public ViewPostService(DatabaseContext context, ISettingsService settingsService, IContactService contactService, ISharedService sharedService, IGroupService groupService,IProfileService profileService, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _settingsService = settingsService;
            _contactService = contactService;
            _groupService = groupService;
            _sharedService = sharedService;
            _httpContextAccessor = httpContextAccessor;
            _profileService = profileService;
        }


       public PostDetailsModel GetPostDetails(int? postId)
        {
            var result = new PostDetailsModel();
            var post = _context.Set<Post>().Where(x => x.Id == postId).FirstOrDefault();
            result.PostId = postId;
            result.PostContent = post.Content;
            if (post.PublishDate.Date == DateTime.Now.Date)
                result.PostDateTime = "Today " + post.PublishDate.ToString("hh:mm tt");
            else
                result.PostDateTime = post.PublishDate.ToString("dddd, dd MMMM yyyy hh:mm tt");
            result.LikesCount = _context.Set<Interaction>().Where(x => x.PostId == postId && x.InteractionType == (int)InteractionTypeEnum.Like).Select(x => x.UserId).ToList();
            result.SharesCount = _context.Set<Interaction>().Where(x => x.PostId == postId && x.InteractionType == (int)InteractionTypeEnum.Share).Select(x => x.UserId).ToList();

            result.Controllers = new List<PostController>();

            var controllers = _context.Set<U2PRelationship>().Where(x => x.PostId == postId).Include(x => x.User);

            foreach(var item in controllers)
            {
                var controller = new PostController()
                {
                    // controller.PostId = postId;
                    ControllerName = item.User.FullName,
                    ControllerImageUrl = item.User.ImageUrl,
                    ControllerId = item.UserId,
                    Type = item.U2PRelationshipTypeId,
                    U2PRelaionshipId = item.Id,
                    PrivacySettings = _settingsService.GetPostPrivacySetting(item.Id)
                };

                result.Controllers.Add(controller);   
            }
            return result;
        }


        public MyPostsModel GetPosts(string UserId)
        {
           
            var result = new MyPostsModel()
            {
                AllPosts = new List<PostDetailsModel>(),
                AsOwner = new List<PostDetailsModel>(),
                AsContributor = new List<PostDetailsModel>(),
                AsTagged = new List<PostDetailsModel>(),
                AsShareHolder = new List<PostDetailsModel>()
            };

            var u2pPosts = _context.Set<U2PRelationship>().Where(x => x.UserId == UserId && x.PostId != null).OrderByDescending(x => x.PostId);
            if(u2pPosts != null)
            foreach(var u2pPost in u2pPosts)
            {
                var postDetails = GetPostDetails(u2pPost.PostId);
                   
                   // if (postDetails.Controllers != null)
                        postDetails.Controllers.Where(x => x.ControllerId == UserId).FirstOrDefault().IsMe = true;
                    SetPostDescriptionAndImage(postDetails);
                            switch(u2pPost.U2PRelationshipTypeId)
                            {
                                case 1:
                                    {
                                result.AllPosts.Add(postDetails);
                                result.AsOwner.Add(postDetails);
                                        break;
                                    }
                                case 2:
                                    {
                                result.AllPosts.Add(postDetails);
                                result.AsContributor.Add(postDetails);
                                        break;
                                    }
                                case 3:
                                    {
                                result.AllPosts.Add(postDetails);
                                result.AsTagged.Add(postDetails);
                                        break;
                                    }
                                case 4:
                                    {
                                result.AllPosts.Add(postDetails);
                                result.AsShareHolder.Add(postDetails);
                                        break;
                                    }
                                default:
                                    break;
                            }
            }
            return result;
        }

        public MyPostsModel GetMyPosts()
        {
            userId = UserManagerExtensions.GetCurrentUserId(_httpContextAccessor);
            var model = GetPosts(userId);
          //  model.AllPosts = null;
            return model;
        }


        public List<PostDetailsModel> GetAllowedPosts(string requestUser, string targetUser)
        {
            var allowedPosts = new List<PostDetailsModel>();
            var allPosts = GetPosts(targetUser);
            if(allPosts.AllPosts !=null)
            {
                foreach(var post in allPosts.AllPosts)
                {
                   var visible =   CheckVisibility(targetUser, requestUser, post.PostId);
                    if (visible)
                        allowedPosts.Add(post);
                }
            }
            return allowedPosts;

        }

        public PostDetailsModel SetPostDescriptionAndImage(PostDetailsModel post)
        {
            var me = post.Controllers.Where(x => x.IsMe).FirstOrDefault();

            var owner = post.Controllers.Where(x => x.Type == (int)U2PRelationshipTypeEnum.Owner).FirstOrDefault();
            var contributor = post.Controllers.Where(x => x.Type == (int)U2PRelationshipTypeEnum.Contributor).FirstOrDefault();
            var Tagged = post.Controllers.Where(x => x.Type == (int)U2PRelationshipTypeEnum.Tagged);
            switch (me.Type)
            {
                case 1: // owner
                    {
                        if (contributor == null)
                        {
                            post.PostDescription = "You Added a Post";
                            post.DisplayImageURL = owner.ControllerImageUrl;
                        }
                        else
                        {  
                            post.PostDescription = "<a href=\"/UserProfile?cId="+contributor.ControllerId+"\">" + contributor.ControllerName + "</a>  " + "Added a post to your space ";
                            post.DisplayImageURL = contributor.ControllerImageUrl;
                        }
                        if ( Tagged.Count() > 0)
                        {
                            post.PostDescription += " with ";
                            foreach (var t in Tagged)
                                post.PostDescription += "<a href=\"/UserProfile?cId="+t.ControllerId+"\">" + t.ControllerName + "</a> , ";
                            post.PostDescription = post.PostDescription.Remove(post.PostDescription.Length - 3);
                        }
                        break;
                    }
                case 2: // Contributor
                    {
                        post.DisplayImageURL = contributor.ControllerImageUrl;
                        post.PostDescription = "You Added a Post to " + "<a href=\"/UserProfile?cId="+owner.ControllerId+"\">" + owner.ControllerName + "</a> 's space ";
                        if (Tagged.Count() > 0)
                        {
                            post.PostDescription += "with ";
                            foreach (var t in Tagged)
                                post.PostDescription += "<a href=\"/UserProfile?cId="+t.ControllerId+"\">" + t.ControllerName + "</a> , ";
                            post.PostDescription = post.PostDescription.Remove(post.PostDescription.Length - 3);
                        }
                        break;
                    }
                case 3: //Tagged
                    {
                        if (contributor == null)
                        {
                            post.PostDescription = "<a href=\"/UserProfile?cId="+owner.ControllerId+"\">" + owner.ControllerName + "</a> Tagged you to this Post ";
                            post.DisplayImageURL = owner.ControllerImageUrl; 
                        }
                        else
                        {
                            post.PostDescription = "<a href=\"/UserProfile?cId="+contributor.ControllerId+"\">" + contributor.ControllerName + "</a>  Tagged you to this Post ";
                            post.DisplayImageURL = contributor.ControllerImageUrl;
                        }
                        if (Tagged.Where(x => !x.IsMe).Count() >0)
                        {
                            post.PostDescription += "with ";
                            foreach (var t in Tagged.Where(x => !x.IsMe))
                                post.PostDescription += "<a href=\"/UserProfile?cId="+t.ControllerId+"\">" + t.ControllerName + "</a> , ";
                            post.PostDescription = post.PostDescription.Remove(post.PostDescription.Length - 3);
                        }
                        break;
                    }
                case 4: // share
                    {
                        post.DisplayImageURL = me.ControllerImageUrl;
                        post.PostDescription = "You shared a post of <a href =\"/UserProfile?cId=" + owner.ControllerId + "\">" + owner.ControllerName + "</a> ";
                        if (contributor != null)
                            post.PostDescription += " and  <a href =\"/UserProfile?cId="+contributor.ControllerId+"\">" + contributor.ControllerName + "</a> ";
                        if (Tagged.Count() > 0 )
                        {
                            post.PostDescription += "with ";
                            foreach (var t in Tagged)
                                post.PostDescription += "<a href=\"/UserProfile?cId="+t.ControllerId+"\">" + t.ControllerName + "</a> , ";
                            post.PostDescription = post.PostDescription.Remove(post.PostDescription.Length - 3);
                        }
                        break;
                    }
                default:
                    break;
            }
            return post;
        }


        public bool CheckVisibility(string targetUser, string requestUser, int? postId)
        {
            if(targetUser == null)
                targetUser = UserManagerExtensions.GetCurrentUserId(_httpContextAccessor);

            var controllers = _context.Set<U2PRelationship>().Where(x => x.PostId == postId && x.PostId != null);

            if (controllers.Select(x => x.UserId) .Contains(requestUser)) // if user is one of post's conrollers
                return true;
            int result = 0;//sum of decisins
            var total = 0; // number of original controllers
            foreach (var controller in controllers.Where(x => x.U2PRelationshipTypeId != (int)U2PRelationshipTypeEnum.ShareHolder).Select(x => x.Id))
            {
                result += GetDecision(requestUser, controller);
                total += 1;
            }   
            if(controllers.Where(x => x.UserId == targetUser).FirstOrDefault().U2PRelationshipTypeId==(int) U2PRelationshipTypeEnum.ShareHolder )
            {
                result += GetDecision(requestUser, controllers.Where(x => x.UserId == targetUser).FirstOrDefault().Id);
                total += 1;
            }
            var policy = _context.Set<ConflictResolutionPolicy>().Where(x => x.Active).Select(x => x.PolicyCode).FirstOrDefault();
            switch(policy)
            {
                case "All":
                    {
                        if (result == total)
                            return true;
                        break;
                    }
                case "One":
                    {
                        if (result >= 1)
                            return true;
                        break;
                    }
                case "Majority":
                    {
                        if (result >= total/2)
                            return true;
                        break;
                    }
                default:
                    break;
            }
            return false;    
        }

        public int GetDecision(string user, int u2pId) // 0 disallow -- 1 allow
        {
            var allowedEntities = _context.Set<PrivacySetting>().Where(x => x.U2PRelationshipId == u2pId).Include(x => x.SocialEntity);
            var disallowedEntities = _context.Set<DisallowedEntity>().Where(x => allowedEntities.Select(y => y.Id).Contains(x.PrivacySettingId)).Select(x => x.DisallowedUserId);

            if (allowedEntities == null)
                return 1;
            //check disallowed entities
            if (disallowedEntities != null && disallowedEntities.Contains(user))
                return 0;
            // check individuals allowed
            if (allowedEntities.Select(x => x.SocialEntityId).Contains(user))
                return 1;
            //check groups
            var groups = _groupService.GetMyGroups(user).Select(x => x.Id);
            foreach(var group in groups)
               if (allowedEntities.Select(x => x.SocialEntityId) .Contains(group))
                return 1;
            //check relationship
            var postUser = _context.Set<U2PRelationship>().Where(x => x.Id == u2pId).Select(x => x.UserId).FirstOrDefault();
            var relationship = _context.Set<U2URelationship>().Where(x => x.UserId == postUser && x.ContactId == user).Select(x => x.RelationshipTypeId).FirstOrDefault();
            if (relationship!= null && allowedEntities.Select(x => x.SocialEntityId).Contains(relationship))
                return 1;

            return 0;
        }


        public async Task<bool> AddInteraction(int postId, string user, int type)
        {
            var interaction = new Interaction
            {
                PostId = postId,
                UserId = user,
                InteractionType = type
            };
            _context.Add(interaction);
            var result = await _context.SaveChangesAsync();
            return result == 1;
        }
    }
}
