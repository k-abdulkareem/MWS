using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MWS_SocialNetwork.ViewModels
{
    public class WorkViewModel
    {
        public int Id { get; set; }
        public string JobTitle { get; set; }
        public string CompanyName { get; set; }
        public string Since { get; set; }
        public int CountryId { get; set; }
    }
}
