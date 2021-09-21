using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MWS_SocialNetwork.Models
{
    public class NotificationType
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
    }
}
