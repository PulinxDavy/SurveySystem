using SurveySystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SurveySystem.ViewModels
{
    public class EditQuestionGroupVM
    {

        public QuestionGroup SelectedQuestionGroup { get; set; }

        
        public IEnumerable<int> SelectedNewQuestions { get; set; }
        public IEnumerable<int> SelectedQuestions { get; set; }
        public IEnumerable<SelectListItem> QuestionGroupQuestions
        {
            get
            {
                return new SelectList(SelectedQuestionGroup.Questions, "Id", "QuestionString");
            }   
        }


        public IEnumerable<SelectListItem> Questions
        {
            get
            {
                return new SelectList(AllQuestions, "Id", "QuestionString");
            }
        }
        public List<Question> AllQuestions { get; set; }
    }
}