﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurveySystem.Models
{
    public class Agreement : AppreciationQ
    {
        public List<string> Choices { get; set; }

        public Agreement()
        {
            Choices = new List<string>();
            Choices.Add("Akkoord");
            Choices.Add("Niet Akkoord");
            Choices.Add("Geen Mening");
        }

        public override List<string> Answers
        {
            get { return Choices; }
        }

        public override string AppreciationType
        {
            get { return "Agreement"; }
        }
    }
}