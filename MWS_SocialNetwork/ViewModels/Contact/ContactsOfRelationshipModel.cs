using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MWS_SocialNetwork.ViewModels
{ 
    public class ContactsOfRelationshipModel
    {
        public string Id { get; set; }
        public string RelationshipTitle { get; set; }
        public int Count { get; set; }
        public List<ContactModel> ContactsList { get; set; }
    }
}
