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
        
    }
}