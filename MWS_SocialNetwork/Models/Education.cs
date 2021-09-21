using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MWS_SocialNetwork.Models
{
    public class Education
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public int EducationDegreeId { get; set; } = 1;
        public virtual EducationDegree EducationDegree { get; set; }
        public string StudyTitle { get; set; }
        public string SchoolOrUniversity { get; set; }
        public int CountryId { get; set; } = 1;
        public virtual Country Country { get; set; }
        public DateTime? StartAt { get; set; }
        public DateTime? GraduateAt { get; set; }
    }
}
