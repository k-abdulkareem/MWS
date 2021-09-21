using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MWS_SocialNetwork.Models
{
    public class U2URelationship // user to user relationship
    {
        [key]
        public int id { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public string ContactId { get; set; }
        public virtual ApplicationUser Contact { get; set; }
        public string RelationshipTypeId { get; set; }
        public virtual RelationshipType RelationshipType { get; set; }
        public DateTime Date { get; set; }

    }
}
