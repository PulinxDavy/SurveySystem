using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurveySystem.Models
{
    public class SurveyResult
    {
        public int Id { get; set; }

        public List<string> Answers { get; set; }

        public int SurveyId { get; set; }

        public int UserId { get; set; }

        public int QuestionId { get; set; }
    }
}