using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace SurveySystem.Models
{
    public class User : ApplicationUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Cellphone { get; set; }

    }
}