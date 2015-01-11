using SurveySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurveySystem.ViewModels
{
    public class QuestionShowVM
    {
        public List<Question> Questions { get; set; }
        public List<SurveyResult> SurveyResults { get; set; }
        public Question CurrentQuestion { get; set; }
        public int CurrentQuestionIndex { get; set; }
    }
}