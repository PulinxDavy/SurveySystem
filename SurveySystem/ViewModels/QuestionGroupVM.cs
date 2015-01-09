using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SurveySystem.Models;
using System.ComponentModel.DataAnnotations;

namespace SurveySystem.ViewModels
{
    public class QuestionGroupVM
    {
        [Required]
        public string Title { get; set; }

     //   [Display(Name = "Questions")]
        public int SelectedQuestionId { get; set; }
      //  public Question Question { get; set; }
        public IEnumerable<int> SelectedQuestions { get; set; }
         [Display(Name = "Questions")]
        public IEnumerable<SelectListItem> Questions
        {
            get {
                return new SelectList(AllQuestions, "Id", "QuestionString"); 
            }
        }
        public List<Question> AllQuestions { get; set; }

    }
}