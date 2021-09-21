using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MWS_SocialNetwork.ViewModels
{
    public class ControlViewModel
    {
        [BindProperty]
        public string ActivePolicyCode { get; set; }
        public string[] PoliciesCode { get; set; }

        public List<RelationPercentage> Relationships { get; set; }
    }
}
