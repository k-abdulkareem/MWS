using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MWS_SocialNetwork.Models
{
    public class SocialEntity
    {
        [Key]
        public string Id { get; set; }
        public int EntityTypeId { get; set; }
        public virtual EntityType EntityType { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Group Group { get; set; }
        public virtual RelationshipType RelationshipType { get; set;}
    }
}
