using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MWS_SocialNetwork.Models
{
    public class ConflictResolutionPolicy
    {
        [key]
        public int Id { get; set; }
        public string PolicyCode { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; } = false;
    }
}
