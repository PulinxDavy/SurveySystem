using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurveySystem.Models
{
    public class Appreciation : AppreciationQ
    {
        public override string AppreciationType
        {
            get { return "Very bad - Very good"; }
        }
    }
}