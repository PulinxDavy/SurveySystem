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
            Choices.Add("Very bad");
            Choices.Add("bad");
            Choices.Add("Good");
            Choices.Add("Very Good");
        }

        public override List<string> Answers
        {
            get { return Choices; }
        }

        public override string Type
        {
            get { return "Very bad - Very good"; }
        }
    }
}