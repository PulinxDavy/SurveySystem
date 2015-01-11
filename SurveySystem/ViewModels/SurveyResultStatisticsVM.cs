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
        public List<Tuple<int,string>> Answers { get; set; }
        public List<Tuple<int,string>> uniqueAnswersPerQuestion { get; set; }
        public HashSet<Tuple<int, string, int>> countAnswers { get; set; }
    }
}