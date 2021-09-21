using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MWS_SocialNetwork.Models
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }
        public string ToUserId { get; set; }
        public virtual ApplicationUser ToUser { get; set; }
        public string FromUserId { get; set; }
        public virtual ApplicationUser FromUser { get; set; }
        public DateTime NotificationDate { get; set; }
        public int NotificationTypeId { get; set; }
        public virtual NotificationType NotificationType { get; set; }
        public string Parameter { get; set; }
        public bool IsSeen { get; set; } = false;
     }
}
