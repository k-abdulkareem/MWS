using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MWS_SocialNetwork.ViewModels
{
    public class PersonalViewModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string ImageUrl { get; set; }
        
        [BindProperty]
        public string Gender { get; set; }
        public string[] GenderValues = new[] { "Male", "Female" };

        public string BirthDate { get; set; }
        public DateTime? Birth_Date { get; set; }
        public int CountryId { get; set; }
    }
}
