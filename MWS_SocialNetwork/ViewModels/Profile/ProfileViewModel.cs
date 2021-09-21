using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MWS_SocialNetwork.ViewModels
{
    public class ProfileViewModel
    {
        public IFormFile Image { get; set; }
        public PersonalViewModel PersonalViewModel { get; set; }
        public WorkViewModel WorkViewModel { get; set; }
        public EducationViewModel EducationViewModel { get; set; }
        public CountViewModel CountViewModel { get; set; }
        public List<CountryViewModel> Countries { get; set; }
        public List<EducationDegreeViewModel> EducationDegrees  { get; set; }
        public string ActiveTab { get; set; }
    }
    
}
