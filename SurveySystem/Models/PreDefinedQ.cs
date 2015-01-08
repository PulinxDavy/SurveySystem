using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurveySystem.Models
{
    public class PreDefinedQ : Question
    {
        public override string Type
        {
            get { return "PreDefined"; }
        }

        public override string AppreciationType
        {
            get { return "/"; }
        }
    }
}