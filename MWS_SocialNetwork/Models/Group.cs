using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MWS_SocialNetwork.Models
{
    public class Group
    {
        [Key]
        public string Id { get; set; }

        public string GroupName { get; set; }
        public DateTime CreationDate { get; set; }
        public string ImageUrl { get; set; }

        public string UserId { get; set; } //Creator of Group
        public virtual ApplicationUser  User { get; set; }
        public virtual SocialEntity SocialEntity { get; set; }
    }
}
