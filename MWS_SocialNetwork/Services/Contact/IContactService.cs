using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MWS_SocialNetwork.ViewModels;

namespace MWS_SocialNetwork.Services

{
  public  interface IContactService
    {
        MyContactsViewModel Get();
        List<ContactModel> GetAllContacts();
        List<ContactsOfRelationshipModel> GetContactsOfRelationships(string userId);
        List<ContactModel> FindContact( string text);

    }
}
