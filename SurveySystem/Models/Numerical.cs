using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurveySystem.Models
{
    public class Numerical : AppreciationQ
    {
        public List<string> Choices { get; set; }

        public Numerical()
        {
            Choices = new List<string>();
            for (int i = 1; i <= 10; i++)
            {
                Choices.Add(i.ToString());
            }
        }

        public override List<string> Answers
        {
            get { return Choices; }
        }

        public override string AppreciationType
        {
            get { return "Numerical"; }
        }
    }
}