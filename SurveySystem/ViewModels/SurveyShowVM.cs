using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SurveySystem.Models;

namespace SurveySystem.ViewModels
{
    public class SurveyShowVM
    {
        public int Id { get; set; }

        public Survey Survey { get; set; }

    }
}