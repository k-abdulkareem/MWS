using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MWS_SocialNetwork.ViewModels
{
    public class CreateGroupModel
    {
        public string Name { get; set; }
        public IFormFile Image { get; set; }
        public List<ContactModel> MyContacts { get; set; }
        public string[] SelectedValues { get; set; }
        public List<ContactModel> InvitedContacts { get; set; }

    }
}
