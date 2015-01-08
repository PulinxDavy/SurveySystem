using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurveySystem.Models
{
    public class OpenEndQ : Question
    {
        public override string Type
        {
            get { return "OpenEnd"; }
        }

        public override string AppreciationType
        {
            get { return "/"; }
        }
    }
}