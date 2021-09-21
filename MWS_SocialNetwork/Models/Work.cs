using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MWS_SocialNetwork.Models
{
    public class Work
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public string JobTitle { get; set; }
        public string CompanyName { get; set; }
        public DateTime? Sicne { get; set; }
        public int CountryId { get; set; } = 1;
        public virtual Country Country { get; set; }
    }
}
