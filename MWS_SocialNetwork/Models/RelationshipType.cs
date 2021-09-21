using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MWS_SocialNetwork.Models
{
    public class RelationshipType
    {
        [Key]
        public string Id { get; set; }
        public string Title { get; set; }
        public virtual SocialEntity SocialEntity { get; set; }
        public int Order { get; set; }
    }
}
