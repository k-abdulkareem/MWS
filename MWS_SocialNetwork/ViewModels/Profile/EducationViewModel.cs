using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MWS_SocialNetwork.ViewModels
{
    public class EducationViewModel
    {
        public int Id { get; set; }
        public int EducationDegreeId { get; set; }
        public string StudyTitle { get; set; }
        public string SchoolOrUniversity { get; set; }
        public int CountryId { get; set; }
        public string StartAt { get; set; }
        public string GraduateAt { get; set; }
    }
}
