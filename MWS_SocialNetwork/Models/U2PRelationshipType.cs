using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MWS_SocialNetwork.Models
{
    public class U2PRelationshipType
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Caption { get; set; }
    }
}
