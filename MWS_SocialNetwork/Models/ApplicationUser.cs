using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MWS_SocialNetwork.Models
{
    public class ApplicationUser : IdentityUser
    {
       
        public string FullName { get; set; }
        public string ImageUrl { get; set; }
        public string Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public int CountryId { get; set; } = 1;
        public virtual Country  Country { get; set; }
        public bool IsAdmin { get; set; } = false;
        public virtual SocialEntity SocialEntity { get; set; }
       // public string UserName { get; set; }
       // public string Password { get; set; }
       // public string Email { get; set; }
    }
}
