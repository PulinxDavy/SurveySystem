﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurveySystem.Models
{
    public class QuestionGroup
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public virtual ICollection<Question> Questions { get; set; }

        public virtual ICollection<Survey> Surveys { get; set; }

        public QuestionGroup()
        {
            this.Questions = new List<Question>();
            this.Surveys = new List<Survey>();
        }
    }
}