using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MWS_SocialNetwork.Models
{
    public class U2PRelationship
    {
        [Key]
        public int Id { get; set; }
        public int? PostId { get; set; } // null for Default Settings
        public virtual Post Post { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public int U2PRelationshipTypeId { get; set; }
        public virtual U2PRelationshipType U2PRelationshipType { get; set; }
    }
}
