using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurveySystem.Models
{
    public class Agreement : AppreciationQ
    {
        public override string AppreciationType
        {
            get { return "Agreement"; }
        }
    }
}