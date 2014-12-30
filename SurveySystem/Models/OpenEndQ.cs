using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurveySystem.Models
{
    public class OpenEndQ : Question
    {
        public string AnswerO { get; set; }

        public override object Answer
        {
            get { return AnswerO; }
        }

        public override string Type
        {
            get { return "OpenEnd"; }
        }
    }
}