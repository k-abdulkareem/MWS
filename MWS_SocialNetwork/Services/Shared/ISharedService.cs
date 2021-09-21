using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MWS_SocialNetwork.ViewModels;

namespace MWS_SocialNetwork.Services
{
  public  interface ISharedService
    {
        List<CountryViewModel> GetCountries();
        List<EducationDegreeViewModel> GetEducationDegrees();
        List<RelationshipTypeViewModel> GetRelationships();
        //List<U2PRelationshipSetting> GetU2PRelaytionships();
    }
}
