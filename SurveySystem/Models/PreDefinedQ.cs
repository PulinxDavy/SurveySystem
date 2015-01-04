using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurveySystem.Models
{
    public class PreDefinedQ : Question
    {
        public List<string> Answers { get; set; }

        public override List<string> Answer
        {
            get { return Answers; }
        }

        public override string Type
        {
            get { return "PreDefined"; }
        }
    }
}