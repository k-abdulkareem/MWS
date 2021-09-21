using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MWS_SocialNetwork.Models
{
    public class Permission
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public int PermissionTypeId { get; set; }
        public virtual PermissionType PermissionType { get; set; }
        public string RelationshipTypeId { get; set; }
        public virtual RelationshipType RelationshipType { get; set; }

    }
}
