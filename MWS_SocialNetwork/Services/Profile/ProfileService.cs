using MWS_SocialNetwork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MWS_SocialNetwork.Data;
using MWS_SocialNetwork.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using MWS_SocialNetwork.Settings;

namespace MWS_SocialNetwork.Services
{
   
    public class ProfileService: IProfileService
    {
        private readonly DatabaseContext _context;
        private readonly ISharedService _sharedServie;
        private readonly INotificationService _notificationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private  string userId;
    

        public ProfileService(DatabaseContext context, ISharedService sharedServie,INotificationService notificationService, IHttpContextAccessor httpContextAccessor , UserManager<ApplicationUser> userManager , SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _sharedServie = sharedServie;
            _notificationService = notificationService;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string GetUserId()
        {
            var user = UserManagerExtensions.GetCurrentUserId(_httpContextAccessor);
            return user;
        }

        public List<ContactModel> GetAllUsersExcept(string userId)
        {
           // userId = UserManagerExtensions.GetCurrentUserId(_httpContextAccessor);
            var users = _context.Set<ApplicationUser>().Where(x => x.Id != userId).OrderBy(x => x.FullName)
               .Select(x => new ContactModel {
                   Id = x.Id,
                   FullName = x.FullName,
                   ImageUrl = x.ImageUrl,
                   RelationshipTitle = null
               });
            return users.ToList();
        }

        public ProfileViewModel GetMyProfile()
        {
           userId = UserManagerExtensions.GetCurrentUserId(_httpContextAccessor);
            return GetProfile(userId);
        }

       public ProfileViewModel GetProfile(string id)
        {
            var personal =  _context.ApplicationUsers.Single(x => x.Id == id);
            var work = _context.Work.Single(x => x.UserId == id);
            var education = _context.Education.Single(x => x.UserId == id);

            var personalModel = new PersonalViewModel
            {
                Id = id,
                FullName =  personal.FullName,
                ImageUrl = personal.ImageUrl,
                BirthDate = personal.BirthDate != null ? personal.BirthDate.Value.ToString("dd/MM/yyyy") : "",
                Birth_Date = personal.BirthDate,
                Gender = personal.Gender,
                CountryId = personal.CountryId

            };

            var workModel = new WorkViewModel
            {
                Id = work.Id,
                JobTitle = work.JobTitle,
                CompanyName = work.CompanyName,
                Since = work.Sicne != null ? work.Sicne.Value.ToString("dd/MM/yyyy") : "",
                CountryId = work.CountryId 
            };

            var educationModel = new EducationViewModel
            {
                Id = education.Id,
                EducationDegreeId = education.EducationDegreeId,
                StudyTitle = education.StudyTitle,
                SchoolOrUniversity = education.SchoolOrUniversity,
                CountryId = education.CountryId,

                StartAt = education.StartAt != null ? education.StartAt.Value.ToString("dd/MM/yyyy") : "",
                GraduateAt = education.GraduateAt != null ? education.GraduateAt.Value.ToString("dd/MM/yyyy") : "",
            };

            var countModel = GetCounts(id);

            var profile = new ProfileViewModel
            {
                PersonalViewModel = personalModel,
                WorkViewModel = workModel,
                EducationViewModel = educationModel,
                CountViewModel = countModel,
                Countries = _sharedServie.GetCountries(),
                EducationDegrees = _sharedServie.GetEducationDegrees()
            };

            return profile;
        }


        public UserProfileViewModel GetUserProfile( string contactId)
        {
            var result = new UserProfileViewModel();

            result.ProfileViewModel = GetProfile(contactId);
            result.RelationshipId = GetRelationshipId( contactId);
            result.Relationships = _sharedServie.GetRelationships();
            
            return result;

        }




        public async Task<string> AddUserEntity()
        {
            var entity = new SocialEntity { Id= Guid.NewGuid().ToString() , EntityTypeId = _context.Set<EntityType>().Where(x => x.Title == "User").First().Id };
            _context.Add(entity);
            var result = await _context.SaveChangesAsync();
            if (result == 1)
                return entity.Id;
            else
                return null;
        }

        public async Task<bool> AddWork(string userId)
        {
                var work = new Work { UserId = userId ,CountryId = 1};
                _context.Add(work);
                var result = await  _context.SaveChangesAsync();
                return result == 1 ;
        }

        public async Task<bool> AddEducation(string userId)
        {
            var education = new Education { UserId = userId  , CountryId = 1 , EducationDegreeId = 1};
            _context.Add(education);
            var result = await _context.SaveChangesAsync();
            return result == 1;
        }

        public async Task<bool> AddDefaultPrivacySettings(string userId)
        {
            var types = _context.Set<U2PRelationshipType>().Select(x => x.Id);
            var settings = new List<U2PRelationship>();
            foreach(var item in types)
            {
                var setting = new U2PRelationship {
                    PostId = null,
                    U2PRelationshipTypeId = item,
                    UserId = userId
                };
                settings.Add(setting);
            }
            _context.AddRange(settings);
            var result = await _context.SaveChangesAsync();
            return result > 0;

        }


        public  async Task<bool> IntializeUserProfile(string userId)
        {

            var result1 =  await AddWork(userId);
            var result2 = await AddEducation(userId);
            var result3 = await AddDefaultPrivacySettings(userId);

            return result1 && result2 && result3 ;

        }


        public async Task<bool> UpdatePersonal(PersonalViewModel model)
        {
            var record = _context.Set<ApplicationUser>().Where(x => x.Id == model.Id).Single();

            record.FullName = model.FullName;
            record.Gender = model.Gender;
            if (!String.IsNullOrEmpty(model.BirthDate))
                record.BirthDate = Convert.ToDateTime(model.BirthDate);
            record.CountryId = model.CountryId;
           

            // var identity = new ClaimsIdentity(_httpContextAccessor.HttpContext.User.Identity);
            //await _userManager.RemoveClaimAsync(record,identity.FindFirst(x => x.Type == "FullName"));
            //await _userManager.AddClaimAsync(record, new Claim("FullName", model.FullName));
            await _signInManager.SignInAsync(record, isPersistent: false);

            _context.Update(record);

            var result = await _context.SaveChangesAsync();
            return result == 1;
        }


        public async Task<bool> UpdateWork(WorkViewModel model)
        {
            var record = _context.Set<Work>().Where(x => x.Id == model.Id).Single();
            record.JobTitle = model.JobTitle;
            record.CompanyName = model.CompanyName;
            record.CountryId = model.CountryId;
            if (!String.IsNullOrEmpty(model.Since))
                record.Sicne = Convert.ToDateTime( model.Since);
            _context.Update(record);
            var result = await _context.SaveChangesAsync();
            return result == 1;
        }


        public async Task<bool> UpdateEducation(EducationViewModel model)
        {
            var record = _context.Set<Education>().Where(x => x.Id == model.Id).Single();
            record.EducationDegreeId = model.EducationDegreeId;
            record.StudyTitle = model.StudyTitle;
            record.CountryId = model.CountryId;
            record.SchoolOrUniversity = model.SchoolOrUniversity;
            if (!String.IsNullOrEmpty(model.GraduateAt))
                record.GraduateAt = Convert.ToDateTime( model.GraduateAt);
            if (!String.IsNullOrEmpty(model.StartAt))
                record.StartAt = Convert.ToDateTime( model.StartAt);
            _context.Update(record);
            var result = await _context.SaveChangesAsync();
            return result == 1;
        } 


        public CountViewModel GetCounts(string id)
        {
            var contacts = _context.Set<U2URelationship>().Where(x => x.UserId == id).ToList().Count();
            var groups = _context.Set<GroupMember>().Where(x => x.UserId == id).ToList().Count();
            var posts = _context.Set<U2PRelationship>().Where(x => x.UserId == id && x.U2PRelationshipTypeId == (Int32)U2PRelationshipTypeEnum.Owner && x.PostId != null).ToList().Count();

            var result = new CountViewModel
            {
                ContactsCount = contacts,
                GroupsCount = groups,
                PostsCount = posts
            };
            return result;
        }

        public async Task<bool> ChangeImage(string fileName , string user_id)
        {
           // userId = UserManagerExtensions.GetCurrentUserId(_httpContextAccessor);
            var user = _context.Set<ApplicationUser>().Where(x => x.Id == user_id).Single();
            
            user.ImageUrl = "/Profiles-Images/" +fileName;

           // var identity = new ClaimsIdentity(_httpContextAccessor.HttpContext.User.Identity);
            //await _userManager.RemoveClaimAsync(user, identity.FindFirst(x => x.Type == "ImageUrl"));
            //await _userManager.AddClaimAsync(user, new Claim("ImageUrl",user.ImageUrl));
            await _signInManager.SignInAsync(user, isPersistent: false);

            _context.Update(user);
            var result = await _context.SaveChangesAsync();
            return result == 1;
        }


        public string GetRelationshipId( string contactId)
        {
            userId = UserManagerExtensions.GetCurrentUserId(_httpContextAccessor);
            var relation = _context.Set<U2URelationship>()
                .Where(x => x.UserId == userId && x.ContactId == contactId)
                .Select(x => x.RelationshipTypeId).FirstOrDefault();
            if (relation == null)
                return "Stranger";
            else
                return relation;
        }


        public async Task<bool> ChangeRelationship(string contactId, string newRelation)
        {
            userId = UserManagerExtensions.GetCurrentUserId(_httpContextAccessor);
            var oldRelation = _context.Set<U2URelationship>()
               .Where(x => x.UserId == userId && x.ContactId == contactId)
               .FirstOrDefault();
            if (newRelation == null)
                return false;
            if (newRelation == "Stranger")
            {
                  _context.Remove(oldRelation);
                var result = await _context.SaveChangesAsync();
                return result == 1;
            }
            if (newRelation != "Stranger" && oldRelation != null )
            {
                oldRelation.RelationshipTypeId = newRelation;
                oldRelation.Date = DateTime.Now.Date;
                _context.Update(oldRelation);
                var result = await _context.SaveChangesAsync();
                var parameter = _context.Set<RelationshipType>().Where(x => x.Id == newRelation).Select(x => x.Title).FirstOrDefault();
                await _notificationService.AddNotification(userId, contactId, parameter, (int)NotificationCodesEnum.ChangeRelationship);
                return result == 1;
            }
            if (newRelation != "Stranger" && oldRelation == null)
            {
                var u2uRelation = new U2URelationship
                {
                    UserId = userId,
                    ContactId = contactId ,
                    Date = DateTime.Now.Date,
                    RelationshipTypeId = newRelation
                };
                _context.Add(u2uRelation);
                var result = await _context.SaveChangesAsync();
                var parameter = _context.Set<RelationshipType>().Where(x => x.Id == newRelation).Select(x => x.Title).FirstOrDefault();
                await _notificationService.AddNotification(userId, contactId, parameter, (int)NotificationCodesEnum.ChangeRelationship);
                return result == 1;
            }
            return false;



        }
    }
}
