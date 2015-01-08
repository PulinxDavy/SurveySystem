using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurveySystem.Models
{
    public class MultipleValuesQ : Question
    {
        public override string Type
        {
            get { return "MultipleValues"; }
        }

        public override string AppreciationType
        {
            get { return "/"; }
        }
    }
}