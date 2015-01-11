using SurveySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurveySystem.ViewModels
{
    public class SurveyResultStatisticsVM
    {
        public List<SurveyResult> SurveyResults { get; set; }
        public List<Question> SurveyQuestions { get; set; }
        public List<string> Answers { get; set; }
    }
}