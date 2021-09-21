using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MWS_SocialNetwork.Models
{
    public class GroupMember
    {
        [Key]
        public int Id { get; set; }

        public string GroupId { get; set; }
        public virtual Group Group { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public DateTime Since { get; set; }
    }
}
