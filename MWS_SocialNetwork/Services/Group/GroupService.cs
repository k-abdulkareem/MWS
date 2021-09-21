using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MWS_SocialNetwork.ViewModels;
using MWS_SocialNetwork.Models;
using MWS_SocialNetwork.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using MWS_SocialNetwork.Settings;

namespace MWS_SocialNetwork.Services
{
    public class GroupService:IGroupService
    {
        private readonly DatabaseContext _context;
        private readonly IContactService _ContactService;
        private readonly INotificationService _notificationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private  string  userId;


        public GroupService(DatabaseContext context , IHttpContextAccessor httpContextAccessor , IContactService ContactService,INotificationService notificationService)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _ContactService = ContactService;
            _notificationService = notificationService;
             
        }


        public MyGroupViewModel Get()
        {
            userId = UserManagerExtensions.GetCurrentUserId(_httpContextAccessor);
            var createModel = new CreateGroupModel
            {
                MyContacts = _ContactService.GetAllContacts(),
                Name = null,
                Image = null ,
                InvitedContacts = null 
            };

            var model = new MyGroupViewModel();

            model.MyGroups = GetMyGroups(userId);
            model.MyGroupsCount = model.MyGroups.Count;
            model.CreateGroup = createModel;

            return model;

        }

        
        public  List<GroupModel> GetMyGroups(string userId)
        {
           // userId = UserManagerExtensions.GetCurrentUserId(_httpContextAccessor);

            var group = _context.Set<Group>().ToList();
            var member = _context.Set<GroupMember>()
                .Include(x => x.Group)
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.Since).ToList();

            var result = member.Join(group,m => m.GroupId , g => g.Id 
                  , (m,g) => new  GroupModel{
                     Id = g.Id,
                     ImageUrl = g.ImageUrl,
                     Name = g.GroupName,
                     IsMember = true,
            } ) .ToList();
                
            return result;
         }


        public async Task<string> CreateGroup(CreateGroupModel model)
        {
            userId = UserManagerExtensions.GetCurrentUserId(_httpContextAccessor);
            var entity = new SocialEntity {
               EntityTypeId = (Int32)EntityTypeEnum.Group
            };
            _context.Add(entity);
            var result1 = await _context.SaveChangesAsync();
            if (result1 == 1)
            {
                var group = new Group
                {
                    Id=entity.Id,
                    GroupName = model.Name,
                    //ImageUrl = "/Groups-Images/" + fileName,
                    ImageUrl = null,
                    CreationDate = DateTime.Now.Date,
                    UserId = userId
                };

                _context.Add(group);
                var result2= await _context.SaveChangesAsync();
                if (result2 == 1)
                {
                    var member = new GroupMember
                    {
                        GroupId = group.Id,
                        UserId = userId,
                        Since = DateTime.Now.Date
                    };
                    _context.Add(member);
                    var result3 = await _context.SaveChangesAsync();
                    return group.Id;
                }
                return null;
            }
            else return null;
        }


        public async Task<bool> SetImage(string groupId, string fileName)
        {
            var group = _context.Set<Group>().Where(x => x.Id == groupId).Single();
            group.ImageUrl = "/Groups-Images/" + fileName;
            _context.Update(group);
            var result = await _context.SaveChangesAsync();
            return result == 1;
        }

        public List<GroupModel> FindGroup(string searchString)
        {
            userId = UserManagerExtensions.GetCurrentUserId(_httpContextAccessor);
            var inGroups = GetMyGroups(userId).Where(x => x.Name.ToLower().Contains(searchString.Trim().ToLower()));
            var inGroupsId = inGroups.Select(x => x.Id);

            var outGroups = _context.Set<Group>()
                .Where(x => x.GroupName.Contains(searchString) && !inGroupsId.Contains(x.Id))
                .OrderBy(x => x.GroupName)
                .Select(x => new GroupModel
                {
                    Id = x.Id,
                    Name = x.GroupName,
                    ImageUrl = x.ImageUrl,
                    IsMember = false
                }).ToList();

            var result = inGroups.Concat(outGroups).ToList();
            return result;
        }


        public GroupModel GetGroupDetails(string gId)
        {
            userId = UserManagerExtensions.GetCurrentUserId(_httpContextAccessor);

            var group = _context.Set<Group>().Where(x => x.Id == gId).FirstOrDefault();
            var members = _context.Set<GroupMember>().Where(x => x.GroupId == gId).Include(x => x.User);
                          
            var result = new GroupModel();

            result.Id = group.Id;
            result.Name = group.GroupName;
            result.ImageUrl = group.ImageUrl;
            result.MembersCount = members.ToList().Count;
            result.PostsCount = 0;
            result.CreationDate = group.CreationDate;
            result.Members = members.Select(x => new ContactModel
            {
                Id = x.UserId,
                FullName = x.User.FullName,
                ImageUrl = x.User.ImageUrl,
                RelationshipTitle = null
            }).ToList();
            if (members.Where(x => x.UserId == userId).ToList().Count != 0)
                result.IsMember = true;
            else
                result.IsMember = false;

            return result;

        }


        public async Task<bool> ChangeMembership(string gId, string value)
        {
            userId = UserManagerExtensions.GetCurrentUserId(_httpContextAccessor);
            if (value == "1") // joined
            {
                var member = new GroupMember
                {
                    GroupId = gId,
                    UserId = userId,
                    Since = DateTime.Now.Date
                };
                _context.Add(member);
                var result = await _context.SaveChangesAsync();
                return result == 1;
            }
            if (value == "0")  // disjoined
            {
                var member = _context.Set<GroupMember>().Where(x => x.UserId == userId && x.GroupId == gId).FirstOrDefault();
                _context.Remove(member);
                var result = await _context.SaveChangesAsync();
                return result == 1;
            }
            else
                return false;
        }


        public async Task<bool> SendInvitations(string gId, string[] ids)
        {
            
                userId = UserManagerExtensions.GetCurrentUserId(_httpContextAccessor);
                foreach (var item in ids)
                  await _notificationService.AddNotification(userId, item, gId, (int)NotificationCodesEnum.GroupInvitation);
                return true ;
            
        }
    }
}
