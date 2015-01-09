using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurveySystem.Models
{
    public class Survey
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool Active { get; set; }

        public DateTime ActiveSince { get; set; }

        public bool Anonymous { get; set; }

        public string Comment { get; set; }

        public virtual ICollection<Question> Questions { get; set; }

        public virtual ICollection<QuestionGroup> QuestionGroups { get; set; }

        public Survey()
        {
            this.QuestionGroups = new HashSet<QuestionGroup>();
            this.Questions = new HashSet<Question>();
        }
    }
}