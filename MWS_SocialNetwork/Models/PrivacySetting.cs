using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MWS_SocialNetwork.Models
{
    public class PrivacySetting
    {
        [Key]
        public int Id { get; set; }
        public int U2PRelationshipId { get; set; }
        public virtual U2PRelationship U2PRelationship { get; set; }
        public string SocialEntityId { get; set; } //userId - GroupId - RelationshipTypeId
        public virtual SocialEntity SocialEntity { get; set; }
    }
}