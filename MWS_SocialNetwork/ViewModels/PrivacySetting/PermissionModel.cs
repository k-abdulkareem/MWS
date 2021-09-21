using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MWS_SocialNetwork.ViewModels
{
    public class PermissionModel
    {
        public int Id { get; set; } // pernission type id
        public List<SelectListItem> Relationships { get; set; }
        public List<SelectListItem> ExceptRelationshipsContacts { get; set; }
    }
}
