using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SurveySystem.Models;
using System.Web.Mvc;

namespace SurveySystem.ViewModels
{
    public class SurveyVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A Survey Title is required")]
        public string Title { get; set; }

        public string Description { get; set; }

        public bool Active { get; set; }

        public string Comment { get; set; }

        [Display(Name = "List of existing groups")]
        public virtual List<QuestionGroup> AllGroups { get; set; }

        [Display(Name = "List of existing questions")]
        public virtual List<Question> AllQuestions { get; set; }

        [Display(Name = "List of selected groups")]
        public IEnumerable<SelectListItem> SelectedGroups { get; set; }

        [Display(Name = "List of selected questions")]
        public IEnumerable<SelectListItem> SelectedQuestions { get; set; }

        public IEnumerable<SelectListItem> Groups { get { return new SelectList(AllGroups, "Id", "Title"); } }

        public IEnumerable<SelectListItem> Questions { get { return new SelectList(AllQuestions, "Id", "QuestionString"); } }

        public SurveyVM()
        {
            this.SelectedGroups = new SelectList(new List<SelectListItem>());
            this.SelectedQuestions = new SelectList(new List<SelectListItem>());
        }
        
    }
}