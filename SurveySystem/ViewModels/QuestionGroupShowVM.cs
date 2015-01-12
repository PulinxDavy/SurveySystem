using SurveySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurveySystem.ViewModels
{
    public class QuestionGroupShowVM
    {
        public List<QuestionGroup> QuestionGroups { get; set; }
        public QuestionGroup CurrentQuestionGroup { get; set; }
        public List<SurveyResult> SurveyResults { get; set; }
        public int CurrentQuestionGroupIndex { get; set; }
        public string OtherAnswer { get; set; }
    }
}