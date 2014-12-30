using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SurveySystem.Models;

namespace SurveySystem.ViewModels
{
    public class SurveyVM
    {
        [Required(ErrorMessage = "A Survey Title is required")]
        public string Title { get; set; }

        public string Description { get; set; }

        public bool Active { get; set; }

        public string Comment { get; set; }

        public virtual List<QuestionGroup> Groups { get; set; }

        public virtual List<Question> Questions { get; set; }
        
    }
}