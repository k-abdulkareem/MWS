using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MWS_SocialNetwork.Models;
using MWS_SocialNetwork.ViewModels;


namespace MWS_SocialNetwork.Services
{
 public   interface IProfileService
    {
        string GetUserId();
        List<ContactModel> GetAllUsersExcept(string userId);

        ProfileViewModel GetProfile(string id);
        ProfileViewModel GetMyProfile();

        UserProfileViewModel GetUserProfile( string contactId);

        Task<string> AddUserEntity();
        Task<bool> AddWork(string userId);
        Task<bool> AddEducation(string userId);
        Task<bool> AddDefaultPrivacySettings(string userId);

        Task<bool> IntializeUserProfile(string userId);

        Task<bool> UpdatePersonal(PersonalViewModel model);
        Task<bool> UpdateWork(WorkViewModel model);
        Task<bool> UpdateEducation(EducationViewModel model);
        Task<bool> ChangeImage( string fileName , string user_id);

        

        Task<bool> ChangeRelationship( string contactId, string newRelationshipId);

        string GetRelationshipId( string contactId ); 

        CountViewModel GetCounts(string id);

    }
}
