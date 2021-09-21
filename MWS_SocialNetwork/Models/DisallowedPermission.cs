using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MWS_SocialNetwork.Models
{
    public class DisallowedPermission
    {
        [Key]
        public int Id { get; set; }
        public int PermissionId { get; set; }
        public virtual Permission Permission { get; set; }
        public string  DisallowedUserId {get; set;}
        public virtual ApplicationUser DisallowedUser { get; set; }
    }
}
