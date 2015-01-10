using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SurveySystem.Models;

namespace SurveySystem.ViewModels
{
    public class SurveyEditVM
    {
        public Survey Survey { get; set; }

        [Display(Name = "List of existing groups")]
        public virtual List<QuestionGroup> AllGroups { get; set; }

        [Display(Name = "List of existing questions")]
        public virtual List<Question> AllQuestions { get; set; }

        [Display(Name = "List of selected groups")]
        public IEnumerable<int> SelectedGroups { get; set; }

        [Display(Name = "List of selected questions")]
        public IEnumerable<int> SelectedQuestions { get; set; }

        public IEnumerable<SelectListItem> Groups { get { return new SelectList(AllGroups, "Id", "Title"); } }

        public IEnumerable<SelectListItem> Questions { get { return new SelectList(AllQuestions, "Id", "QuestionString"); } }

        public IEnumerable<SelectListItem> SelGroups { get { return new SelectList(Survey.QuestionGroups, "Id", "Title"); } }

        public IEnumerable<SelectListItem> SelQuestions { get { return new SelectList(Survey.Questions, "Id", "QuestionString"); } }

        public SurveyEditVM()
        {
            this.AllGroups = new List<QuestionGroup>();
            this.AllQuestions = new List<Question>();
        }
    }
}