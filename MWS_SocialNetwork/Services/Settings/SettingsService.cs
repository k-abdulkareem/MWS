using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MWS_SocialNetwork.Models;
using Microsoft.EntityFrameworkCore;
using MWS_SocialNetwork.ViewModels;
using MWS_SocialNetwork.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using MWS_SocialNetwork.Settings;
using System.Web.Mvc;

namespace MWS_SocialNetwork.Services
{
    public class SettingsService :ISettingsService
    {
        private readonly DatabaseContext _context;
        private readonly ISharedService _sharedService;
        private readonly IGroupService _groupService;
        private readonly IProfileService _profileService;
        private readonly IContactService _contactService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string userId;

        public SettingsService(DatabaseContext context , ISharedService sharedService , IGroupService groupService , IProfileService profileService , IContactService contactService, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _sharedService = sharedService;
            _groupService = groupService;
            _profileService = profileService;
            _contactService = contactService;
            _httpContextAccessor = httpContextAccessor;
        }

        public DefaultPrivacySettingsModel Get()
        {
            userId = UserManagerExtensions.GetCurrentUserId(_httpContextAccessor);

            var model = GetDefaultPrivacySetting(userId);
            var model1 = GetPermissions(userId);
            model.ContributingPermission = model1.ContributingPermission;
            model.TaggingPermission = model1.TaggingPermission;
            model.SharingPermission = model1.SharingPermission;
            return model;
        }


        public DefaultPrivacySettingsModel GetPermissions(string userId)
        {
            
            var permissionModel = new DefaultPrivacySettingsModel();

            var contacts = _contactService.GetContactsOfRelationships(userId);

            var pts = _context.Set<PermissionType>();
            foreach (var pt in pts) //3 rows
            {
                var pModel = new PermissionModel();
                pModel.Id = pt.Id;
                //initialize relationships
                pModel.Relationships = _sharedService.GetRelationships().Select(x => new SelectListItem
                {
                    Value = x.Id,
                    Text = x.Title,
                    Selected = false
                }).ToList();
                //initialize Relationships Excepts
                pModel.ExceptRelationshipsContacts = new List<SelectListItem>();

                var ps= _context.Set<Permission>().Where(x => x.PermissionTypeId == pt.Id && x.UserId == userId);
                if(ps != null)
                {
                    foreach(var p in ps)
                    {
                        pModel.Relationships.Find(x => x.Value == p.RelationshipTypeId).Selected = true;
                        //  var group = new SelectListGroup { Name = i.SocialEntity.RelationshipType.Title };
                        if (contacts.Find(x => x.Id == p.RelationshipTypeId).ContactsList != null)
                        {
                            var d = contacts.Find(x => x.Id == p.RelationshipTypeId).ContactsList
                                .Select(x => new SelectListItem
                                {
                                    Value = x.Id,
                                    Text = x.FullName,
                                    Selected = false
                                });

                            pModel.ExceptRelationshipsContacts.AddRange(d);
                        }
                    }
                }
                //disallowed
                var disallowed = _context.Set<DisallowedPermission>().Where(x => ps.Select(y => y.Id).Contains(x.PermissionId)).Select(x => x.DisallowedUserId).ToList();
                pModel.ExceptRelationshipsContacts.FindAll(x => disallowed.Contains(x.Value)).ForEach(x => x.Selected = true);
                // assign to final model
                switch (pt.Id)
                {
                    case (int)PermissionTypeEnum.Contributing:
                        {
                            permissionModel.ContributingPermission = pModel;
                            break;
                        }
                    case (int)PermissionTypeEnum.Tagging:
                        {
                            permissionModel.TaggingPermission = pModel;
                            break;
                        }
                    case (int)PermissionTypeEnum.Sharing:
                        {
                            permissionModel.SharingPermission = pModel;
                            break;
                        }
                    
                }
            }
            return permissionModel;
        }

        public DefaultPrivacySettingsModel GetDefaultPrivacySetting(string userId)
        {
          
            var privacySettingModel = new DefaultPrivacySettingsModel();

            var contacts = _contactService.GetContactsOfRelationships(userId);
            var groups = _groupService.GetMyGroups(userId);
             
            var u2p = _context.Set<U2PRelationship>().Where(x => x.UserId == userId && x.PostId == null)
             .Include(x => x.U2PRelationshipType);

            foreach (var item in u2p) // 4 rows
            {
                var u2pModel = new u2pPrivacySettingModel();

                u2pModel.Id = item.Id;
                //initialize relationships
                u2pModel.Relationships = _sharedService.GetRelationships().Select(x => new SelectListItem
                {
                    Value = x.Id,
                    Text = x.Title,
                    Selected = false
                }).ToList();
                //initialize Relationships Excepts
                u2pModel.ExceptRelationshipsContacts = new List<SelectListItem>();
                //initialize Groups
                u2pModel.Groups = groups.Select(x => new SelectListItem
                {
                    Value = x.Id,
                    Text = x.Name,
                    Selected = false
                }).ToList();
                //initialize Groups Except
                u2pModel.ExceptGroupsMembers = new List<SelectListItem>();
                //initialize individuals
                u2pModel.Individuals = _profileService.GetAllUsersExcept(userId).Select(x => new SelectListItem
                {
                    Value = x.Id,
                    Text = x.FullName,
                    Selected = false
                }).ToList();
                //get selected entiies
                var ps = _context.Set<PrivacySetting>().Where(x => x.U2PRelationshipId == item.Id)
                 .Include(x => x.SocialEntity);
               
                if (ps != null)
                {
                    foreach (var i in ps)
                    {
                        switch (i.SocialEntity.EntityTypeId)
                        {
                            case (int)EntityTypeEnum.Relationship:
                                {
                                    u2pModel.Relationships.Find(x => x.Value == i.SocialEntityId).Selected = true;
                                    //  var group = new SelectListGroup { Name = i.SocialEntity.RelationshipType.Title };
                                    if(contacts.Find(x => x.Id == i.SocialEntityId).ContactsList != null)
                                    {
                                        var d = contacts.Find(x => x.Id == i.SocialEntityId).ContactsList
                                            .Select(x => new SelectListItem
                                            {
                                                Value = x.Id,
                                                Text = x.FullName,
                                            // Group = group,
                                            Selected = false
                                            });

                                        u2pModel.ExceptRelationshipsContacts.AddRange(d);
                                    }
                                    
                                    //var disallowed = _context.Set<DisallowedEntity>().Where(x => x.PrivacySettingId == i.Id).Include(x => x.DisallowedUser).Select(x => x.DisallowedUserId).ToList();
                                    //u2pModel.ExceptRelationshipsContacts.FindAll(x => disallowed.Contains(x.Value)).ForEach(x => x.Selected = true);
                                    break;
                                }
                            case (int)EntityTypeEnum.Group:
                                {
                                    u2pModel.Groups.Find(x => x.Value == i.SocialEntityId).Selected = true;
                                    // var group = new SelectListGroup { Name = i.SocialEntity.Group.GroupName };
                                    var members = _groupService.GetGroupDetails(i.SocialEntityId).Members;
                                    if (members != null)
                                    {
                                        var m = members.Select(x => new SelectListItem
                                       {
                                           Value = x.Id,
                                           Text = x.FullName,
                                            // Group = group,
                                            Selected = false
                                       });
                                       u2pModel.ExceptGroupsMembers.AddRange(m);
                                    }
                                   
                                    //var disallowed = _context.Set<DisallowedEntity>().Where(x => x.PrivacySettingId == i.Id).Include(x => x.DisallowedUser).Select(x => x.DisallowedUserId).ToList();
                                    //u2pModel.ExceptGroupsMembers.FindAll(x => disallowed.Contains(x.Value)).ForEach(x => x.Selected = true);
                                    break;
                                }
                            case (int)EntityTypeEnum.User:
                                {
                                    u2pModel.Individuals.Find(x => x.Value == i.SocialEntityId).Selected = true;
                                    break;
                                }
                            default:
                                break;
                        }
                    }
                }
                //assign exception
                var disallowed_r = _context.Set<DisallowedEntity>().Where(x => ps.Select(y => y.Id).Contains(x.PrivacySettingId) && x.PrivacySetting.SocialEntity.EntityTypeId ==(int)EntityTypeEnum.Relationship).Include(x => x.DisallowedUser).Select(x => x.DisallowedUserId).ToList();
                    u2pModel.ExceptRelationshipsContacts.FindAll(x => disallowed_r.Contains(x.Value)).ForEach(x => x.Selected = true);
                
                var disallowed_g = _context.Set<DisallowedEntity>().Where(x => ps.Select(y => y.Id).Contains(x.PrivacySettingId) && x.PrivacySetting.SocialEntity.EntityTypeId == (int)EntityTypeEnum.Group).Include(x => x.DisallowedUser).Select(x => x.DisallowedUserId).ToList();
                    u2pModel.ExceptGroupsMembers = u2pModel.ExceptGroupsMembers.GroupBy(x => x.Value).Select(x => x.First()).ToList(); // remove dublicate
                    var u = u2pModel.ExceptGroupsMembers.Find(x => x.Value == userId);
                    u2pModel.ExceptGroupsMembers.Remove(u); // remove me
                    u2pModel.ExceptGroupsMembers.FindAll(x => disallowed_g.Contains(x.Value)).ForEach(x => x.Selected = true);


                // assign to final model
                switch (item.U2PRelationshipTypeId)
                {
                    case (int)U2PRelationshipTypeEnum.Owner:
                        {
                            privacySettingModel.OwnerPrivacySettings = u2pModel;
                            break;
                        }
                    case (int)U2PRelationshipTypeEnum.Contributor:
                        {
                            privacySettingModel.ContributorPrivacySettings = u2pModel;
                            break;
                        }
                    case (int)U2PRelationshipTypeEnum.Tagged:
                        {
                            privacySettingModel.TaggedPrivacySettings = u2pModel;
                            break;
                        }
                    case (int)U2PRelationshipTypeEnum.ShareHolder:
                        {
                            privacySettingModel.ShareHolderPrivacySettings = u2pModel;
                            break;
                        }

                 }
               
             }

            return privacySettingModel;
        }


        public u2pPrivacySettingModel GetPostPrivacySetting(int u2pRelationshipId)
        {
            var u2pModel = new u2pPrivacySettingModel();

            var u2p = _context.Set<U2PRelationship>().Where(x => x.Id == u2pRelationshipId).FirstOrDefault();

            var contacts = _contactService.GetContactsOfRelationships(u2p.UserId);
            var groups = _groupService.GetMyGroups(u2p.UserId);


            u2pModel.Id = u2pRelationshipId;
            //initialize relationships
            u2pModel.Relationships = _sharedService.GetRelationships().Select(x => new SelectListItem
            {
                Value = x.Id,
                Text = x.Title,
                Selected = false
            }).ToList();
            //initialize Relationships Excepts
            u2pModel.ExceptRelationshipsContacts = new List<SelectListItem>();
            //initialize Groups
            u2pModel.Groups = groups.Select(x => new SelectListItem
            {
                Value = x.Id,
                Text = x.Name,
                Selected = false
            }).ToList();
            //initialize Groups Except
            u2pModel.ExceptGroupsMembers = new List<SelectListItem>();
            //initialize individuals
            u2pModel.Individuals = _profileService.GetAllUsersExcept(u2p.UserId).Select(x => new SelectListItem
            {
                Value = x.Id,
                Text = x.FullName,
                Selected = false
            }).ToList();
            //get selected entiies
            var ps = _context.Set<PrivacySetting>().Where(x => x.U2PRelationshipId == u2pRelationshipId)
             .Include(x => x.SocialEntity);

            if (ps != null)
            {
                foreach (var i in ps)
                {
                    switch (i.SocialEntity.EntityTypeId)
                    {
                        case (int)EntityTypeEnum.Relationship:
                            {
                                u2pModel.Relationships.Find(x => x.Value == i.SocialEntityId).Selected = true;
                                //  var group = new SelectListGroup { Name = i.SocialEntity.RelationshipType.Title };
                                if (contacts.Find(x => x.Id == i.SocialEntityId).ContactsList != null)
                                {
                                    var d = contacts.Find(x => x.Id == i.SocialEntityId).ContactsList
                                        .Select(x => new SelectListItem
                                        {
                                            Value = x.Id,
                                            Text = x.FullName,
                                                // Group = group,
                                                Selected = false
                                        });

                                    u2pModel.ExceptRelationshipsContacts.AddRange(d);
                                }
                                break;
                            }
                        case (int)EntityTypeEnum.Group:
                            {
                                u2pModel.Groups.Find(x => x.Value == i.SocialEntityId).Selected = true;
                                var members = _groupService.GetGroupDetails(i.SocialEntityId).Members;
                                if (members != null)
                                {
                                    var m = members.Select(x => new SelectListItem
                                    {
                                        Value = x.Id,
                                        Text = x.FullName,
                                        Selected = false
                                    });
                                    u2pModel.ExceptGroupsMembers.AddRange(m);
                                }
                                break;
                            }
                        case (int)EntityTypeEnum.User:
                            {
                                u2pModel.Individuals.Find(x => x.Value == i.SocialEntityId).Selected = true;
                                break;
                            }
                        default:
                            break;
                    }
                }

                //assign exception
                var disallowed_r = _context.Set<DisallowedEntity>().Where(x => ps.Select(y => y.Id).Contains(x.PrivacySettingId) && x.PrivacySetting.SocialEntity.EntityTypeId == 3).Include(x => x.DisallowedUser).Select(x => x.DisallowedUserId).ToList();
                u2pModel.ExceptRelationshipsContacts.FindAll(x => disallowed_r.Contains(x.Value)).ForEach(x => x.Selected = true);

                var disallowed_g = _context.Set<DisallowedEntity>().Where(x => ps.Select(y => y.Id).Contains(x.PrivacySettingId) && x.PrivacySetting.SocialEntity.EntityTypeId == 2).Include(x => x.DisallowedUser).Select(x => x.DisallowedUserId).ToList();
                u2pModel.ExceptGroupsMembers = u2pModel.ExceptGroupsMembers.GroupBy(x => x.Value).Select(x => x.First()).ToList();
                var u = u2pModel.ExceptGroupsMembers.Find(x => x.Value == u2p.UserId);
                u2pModel.ExceptGroupsMembers.Remove(u);
                u2pModel.ExceptGroupsMembers.FindAll(x => disallowed_g.Contains(x.Value)).ForEach(x => x.Selected = true);

            }

            return u2pModel;


        }


        public bool SetPermissions(DefaultPrivacySettingsModel model)
        {
            try
            {
                userId = UserManagerExtensions.GetCurrentUserId(_httpContextAccessor);

                var rows = new List<PermissionModel>();
                rows.Add(model.ContributingPermission);
                rows.Add(model.TaggingPermission);
                rows.Add(model.SharingPermission);

                foreach (var pt in rows)
                {
                    //Remove privious permissions
                    DeletePermissionRow(pt.Id);
                    //set relationships
                    var done = false;
                    if (pt.Relationships != null)
                        foreach (var r in pt.Relationships)
                        {
                            var p = new Permission
                            {
                                PermissionTypeId = pt.Id,
                                RelationshipTypeId = r.Value,
                                UserId = userId
                            };
                            _context.Add(p);
                            if (!done && pt.ExceptRelationshipsContacts != null)
                                foreach (var re in pt.ExceptRelationshipsContacts)
                                {
                                    var de = new DisallowedPermission
                                    {
                                        PermissionId = p.Id,
                                        DisallowedUserId = re.Value
                                    };
                                    _context.Add(de);
                                    done = true;
                                }
                        }
                }
                _context.SaveChanges();

                return true;
            }
            catch(Exception ex)
            {
                Console.Write(ex.Message);
                return false;
            }
        }


        public bool SetDefaultPrivacySetting(DefaultPrivacySettingsModel model)
        {
            //get Ids
            var rows = new List<u2pPrivacySettingModel>();
            rows.Add(model.OwnerPrivacySettings);
            rows.Add(model.ContributorPrivacySettings);
            rows.Add(model.TaggedPrivacySettings);
            rows.Add(model.ShareHolderPrivacySettings);
            
            //loop on them
            foreach(var u2p in rows)
            {
                SetPrivacySettingRow(u2p);
            }

            _context.SaveChanges();
            
            return true;
        }


        public bool SetPrivacySettingRow(u2pPrivacySettingModel model)
        {
            try
            {
                //Remove privious Settings
                DeletePrivacyRow(model.Id);
                //Relationships
                var done_r = false;
                if (model.Relationships != null)
                    foreach (var r in model.Relationships)
                    {
                        var ps = new PrivacySetting
                        {
                            SocialEntityId = r.Value,
                            U2PRelationshipId = model.Id
                        };
                        _context.Add(ps);
                        if (!done_r && model.ExceptRelationshipsContacts != null)
                            foreach (var re in model.ExceptRelationshipsContacts)
                            {
                                var de = new DisallowedEntity
                                {
                                    DisallowedUserId = re.Value,
                                    PrivacySettingId = ps.Id
                                };
                                _context.Add(de);
                                done_r = true;
                            }
                    }
                //Groups
                var done_g = false;
                if (model.Groups != null)
                    foreach (var g in model.Groups)
                    {
                        var ps = new PrivacySetting
                        {
                            SocialEntityId = g.Value,
                            U2PRelationshipId = model.Id
                        };
                        _context.Add(ps);
                        if (!done_g && model.ExceptGroupsMembers != null)
                            foreach (var ge in model.ExceptGroupsMembers)
                            {
                                var de = new DisallowedEntity
                                {
                                    DisallowedUserId = ge.Value,
                                    PrivacySettingId = ps.Id
                                };
                                _context.Add(de);
                                done_g = true;
                            }
                    }
                //Individuals
                if (model.Individuals != null)
                    foreach (var i in model.Individuals)
                    {
                        var ps = new PrivacySetting
                        {
                            SocialEntityId = i.Value,
                            U2PRelationshipId = model.Id
                        };
                        _context.Add(ps);
                    }

                _context.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                Console.Write(ex.Message);
                return false;
            }
        }


        public bool SetPostPrivacySetting(u2pPrivacySettingModel model , string userId , int postId , int u2pType)
        {
            try
            {
                var u2p = new U2PRelationship
                {
                    PostId = postId,
                    UserId = userId,
                    U2PRelationshipTypeId = u2pType
                };
                _context.Add(u2p);
                int result = _context.SaveChanges();
                if (result == 1)
                {
                    //Relationships
                    var done_r = false;
                    if (model.Relationships != null)
                        foreach (var r in model.Relationships)
                        {
                            if (r.Selected)
                            {
                                var ps = new PrivacySetting
                                {
                                    SocialEntityId = r.Value,
                                    U2PRelationshipId = u2p.Id
                                };
                                _context.Add(ps);
                                if (!done_r && model.ExceptRelationshipsContacts != null)
                                    foreach (var re in model.ExceptRelationshipsContacts)
                                    {
                                        if (re.Selected)
                                        {
                                            var de = new DisallowedEntity
                                            {
                                                DisallowedUserId = re.Value,
                                                PrivacySettingId = ps.Id
                                            };
                                            _context.Add(de);
                                            done_r = true;
                                        }

                                    }
                            }

                        }
                    //Groups
                    var done_g = false;
                    if (model.Groups != null)
                        foreach (var g in model.Groups)
                        {
                            if (g.Selected)
                            {
                                var ps = new PrivacySetting
                                {
                                    SocialEntityId = g.Value,
                                    U2PRelationshipId = u2p.Id
                                };
                                _context.Add(ps);
                                if (!done_g && model.ExceptGroupsMembers != null)
                                    foreach (var ge in model.ExceptGroupsMembers)
                                    {
                                        if (ge.Selected)
                                        {
                                            var de = new DisallowedEntity
                                            {
                                                DisallowedUserId = ge.Value,
                                                PrivacySettingId = ps.Id
                                            };
                                            _context.Add(de);
                                            done_g = true;
                                        }

                                    }
                            }

                        }
                    //Individuals
                    if (model.Individuals != null)
                        foreach (var i in model.Individuals)
                        {
                            if (i.Selected)
                            {
                                var ps = new PrivacySetting
                                {
                                    SocialEntityId = i.Value,
                                    U2PRelationshipId = u2p.Id
                                };
                                _context.Add(ps);
                            }

                        }

                    _context.SaveChanges();

                }
                return true;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return false;
            }
        }

        public void DeletePrivacyRow(int u2pRelaionshipId)
        {
            var p = _context.Set<PrivacySetting>().Where(x => x.U2PRelationshipId == u2pRelaionshipId);
            if (p != null)
            {
                foreach (var item in p)
                {
                    var d = _context.Set<DisallowedEntity>().Where(x => x.PrivacySettingId == item.Id);
                    _context.RemoveRange(d);
                }
                _context.RemoveRange(p);
                _context.SaveChanges();
            }
            return;
        }

        public void DeletePermissionRow(int PermissionTypeId)
        {
            userId = UserManagerExtensions.GetCurrentUserId(_httpContextAccessor);
            var p = _context.Set<Permission>().Where(x => x.UserId == userId && x.PermissionTypeId == PermissionTypeId);    
            if(p != null)
            {
                foreach (var item in p)
                {
                    var d = _context.Set<DisallowedPermission>().Where(x => x.PermissionId == item.Id);
                    _context.RemoveRange(d);
                }
                _context.RemoveRange(p);
                _context.SaveChanges();
            }
            return;
        }

        public List<SelectListItem> GetContactsOfRelationships(List<string> RelationshipIds)
        {
            userId = UserManagerExtensions.GetCurrentUserId(_httpContextAccessor);
            var result = new List<SelectListItem>();
            var relations = _contactService.GetContactsOfRelationships(userId).FindAll(x => RelationshipIds.Contains(x.Id));
            if(relations != null)
            {
                foreach (var item in relations)
                {
                    if(item.ContactsList !=null)
                    {
                      //  var group = new SelectListGroup { Name = item.RelationshipTitle };
                        var contacts = item.ContactsList
                                          .Select(x => new SelectListItem
                                          {
                                              Value = x.Id,
                                              Text = x.FullName,
                                              //Group = group,
                                              Selected = false
                                          });
                        result.AddRange(contacts);
                    }
                }
            }
               return result;

        }
        
        public List<SelectListItem> GetGroupsMembers(List<string> groupsId)
        {
            var result = new List<SelectListItem>();
            userId = UserManagerExtensions.GetCurrentUserId(_httpContextAccessor);
            if (groupsId != null)
            {
                foreach (var item in groupsId)
                {
                    var g = _groupService.GetGroupDetails(item);
                    if (g.Members != null)
                    {
                        var members = g.Members.Where( x => x.Id != userId)
                                          .Select(x => new SelectListItem
                                          {
                                              Value = x.Id,
                                              Text = x.FullName,
                                              Selected = false
                                          });
                        result.AddRange(members);
                    }
                }
            }
            return result.GroupBy(x => x.Value).Select(x => x.First()).ToList();
        }

    }
}
