using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurveySystem.Models
{
    public abstract class Question
    {
        public int Id { get; set; }

        public abstract object Answer { get; }

        public abstract string Type { get; }

        public string QuestionString { get; set; }
    }
}