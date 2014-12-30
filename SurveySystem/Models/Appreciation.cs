using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurveySystem.Models
{
    public class Appreciation : AppreciationQ
    {
        public List<string> Choices { get; set; }

        public Appreciation()
        {
            Choices.Add("Zeer Slecht");
            Choices.Add("Slecht");
            Choices.Add("Goed");
            Choices.Add("Zeer Goed");
        }

        public override List<string> Answers
        {
            get { return Choices; }
        }

        public override string Type
        {
            get { return "Appreciation"; }
        }
    }
}