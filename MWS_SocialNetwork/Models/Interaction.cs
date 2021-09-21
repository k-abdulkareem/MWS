using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MWS_SocialNetwork.Models
{
    public class Interaction
    {
        [Key]
        public int Id { get; set; }
        public int PostId { get; set; } // 0 for Default Settings
        public virtual Post Post { get; set; }
        public string UserId { get; set; }
        public int InteractionType { get; set; } // 0 Like -- 1 share
    }
}
