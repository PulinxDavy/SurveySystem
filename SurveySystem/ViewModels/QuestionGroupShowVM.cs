using SurveySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurveySystem.ViewModels
{
    public class QuestionGroupShowVM
    {
        public Survey Survey { get; set; }
        public QuestionGroup QuestionGroup { get; set; }
        public int CurrentQuestionGroupIndex { get; set; }
        public List<Tuple<int, string>> AnswersList { get; set; }
    }
}