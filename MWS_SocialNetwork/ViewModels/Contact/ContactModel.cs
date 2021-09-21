using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MWS_SocialNetwork.ViewModels

{
    public class ContactModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string ImageUrl { get; set; }
        public string RelationshipTitle { get; set; }
        public string RelationshipTypeId { get; set; }
    }
}
