using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MWS_SocialNetwork.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public DateTime PublishDate {get; set;}
       // public DateTime PublishTime { get; set; } = DateTime.Now.ToString("h:mm tt");
        public string Content { get; set; }
    }
}
