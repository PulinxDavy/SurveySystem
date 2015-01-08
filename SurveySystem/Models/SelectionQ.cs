using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurveySystem.Models
{
    public class SelectionQ : Question
    {
        public override string Type
        {
            get { return "Selection"; }
        }

        public override string AppreciationType
        {
            get { return "/"; }
        }
    }
}