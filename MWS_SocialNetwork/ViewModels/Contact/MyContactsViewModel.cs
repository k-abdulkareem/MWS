using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MWS_SocialNetwork.ViewModels
{
    public class MyContactsViewModel
    {
        public int ContatsCount { get; set; }
        public List<ContactModel> ContactsList { get; set; }
        public List<ContactsOfRelationshipModel > ContactsOfRelationship { get; set; }
    }
}
