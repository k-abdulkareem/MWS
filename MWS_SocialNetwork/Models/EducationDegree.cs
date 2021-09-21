using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MWS_SocialNetwork.Models
{
    public class EducationDegree
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
