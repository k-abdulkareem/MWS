using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MWS_SocialNetwork.Models
{
    public class Attachment
    {
        [Key]
        public int Id { get; set; }
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
        public string Path { get; set; }

    }
}
