using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MWS_SocialNetwork.ViewModels
{
    public class NotificationViewModel
    {
        public int Id { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public string Parameter { get; set; }
        public string FromUserFullName { get; set; }
        public string FromUserId { get; set; }
        public bool IsSeen { get; set; }
        public string Code { get; set; }

    }
}
