using System;
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
    }
}