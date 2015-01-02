using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace SurveySystem.Models
{
    public class User : ApplicationUser
    {
        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Cellphone { get; set; }

    }
}