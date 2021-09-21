using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MWS_SocialNetwork.ViewModels;
using MWS_SocialNetwork.Data;
using MWS_SocialNetwork.Models;

namespace MWS_SocialNetwork.Services
{
    public class SharedService :ISharedService
    {
        private readonly DatabaseContext _context;

        public SharedService (DatabaseContext context)
        {
            _context = context;
        }

        public List<CountryViewModel> GetCountries()
        {
            var result = _context.Set<Country>().Select(x => new CountryViewModel { Id = x.Id, Name = x.Name });

            return result.ToList();

        }

        public List<EducationDegreeViewModel> GetEducationDegrees()
        {
            var result = _context.Set<EducationDegree>().Select(x => new EducationDegreeViewModel { Id = x.Id, Name = x.Name }).ToList();
            return result.ToList();
        }

        public List<RelationshipTypeViewModel> GetRelationships()
        {
            var result = _context.Set<RelationshipType>().OrderBy(x => x.Order).Select(x => new RelationshipTypeViewModel  { Id = x.Id, Title = x.Title }).ToList();
            return result.ToList();
        }

        //public List<U2PRelationshipSetting> GetU2PRelaytionships()
        //{
        //    var result = _context.Set<U2PRelationshipType>()
        //        .Select(x => new U2PRelationshipSetting
        //        {
        //            Id = x.Id,
        //            Title = x.Title,
        //            Caption = x.Caption
        //        });
        //    return result.ToList();
        //}
    }
}
