using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurveySystem.Models
{
    public abstract class AppreciationQ : Question
    {
        public override string Type
        {
            get { return "Appreciation"; }
        }
    }
}