using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurveySystem.Models
{
    public class OpenEndQ : Question
    {
        public List<string> AnswerO { get; set; }

        public override List<string> Answer
        {
            get { return AnswerO; }
        }

        public override string Type
        {
            get { return "OpenEnd"; }
        }
    }
}