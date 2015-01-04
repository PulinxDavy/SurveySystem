using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SurveySystem.Models;

namespace SurveySystem.ViewModels
{
    public class QuestionVM
    {
        public Question Question { get; private set; }

        [Required]
        [Display(Name = "Type of Question")]
        public List<string> QuestionTypes { get; set; }

        public int QuestionType { get; set; }

        [Required]
        [Display(Name = "Type of Appreciation")]
        public List<string> AppreciationTypes { get; set; }

        public int AppreciationType { get; set; }

        public QuestionVM()
        {
            this.FillAppreciationTypes();
            this.FillQuestionTypes();
        }

        public QuestionVM(Question question)
        {
            this.Question = question;
            this.FillAppreciationTypes();
            this.FillQuestionTypes();
        }

        private void FillQuestionTypes()
        {
            this.QuestionTypes = new List<string>();
            this.QuestionTypes.Add("Appreciation");
            this.QuestionTypes.Add("MultipleValues");
            this.QuestionTypes.Add("OpenEnd");
            this.QuestionTypes.Add("PreDefined");
            this.QuestionTypes.Add("Selection");
        }

        private void FillAppreciationTypes()
        {
            this.AppreciationTypes = new List<string>();
            this.AppreciationTypes.Add("Very bad - Very good");
            this.AppreciationTypes.Add("Agreement");
            this.AppreciationTypes.Add("Numerical");
        }

        public IEnumerable<SelectListItem> QuestionTypeItems
        {
            get { return new SelectList(QuestionTypes); }
        }

        public IEnumerable<SelectListItem> AppreciationTypeItems
        {
            get { return new SelectList(AppreciationTypes); }
        }
    }
}