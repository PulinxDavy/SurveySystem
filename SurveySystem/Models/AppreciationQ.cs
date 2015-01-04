using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurveySystem.Models
{
    public abstract class AppreciationQ : Question
    {
        public abstract List<string> Answers { get; }

        public override List<string> Answer
        {
            get { return Answers; }
        }

        public override string Type
        {
            get { return "Appreciation"; }
        }
    }
}