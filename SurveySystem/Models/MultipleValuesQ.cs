using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurveySystem.Models
{
    public class MultipleValuesQ : Question
    {
        public List<string> Answers { get; set; }

        public override object Answer
        {
            get { return Answers; }
        }

        public override string Type
        {
            get { return "MultipleValues"; }
        }
    }
}