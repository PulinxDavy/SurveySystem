using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SurveySystem.Models
{
    public abstract class Question
    {
        public int Id { get; set; }

        public abstract List<string> Answer { get; }

        public abstract string Type { get; }

        public abstract string AppreciationType { get; }

        public string QuestionString { get; set; }

        public int QuestionGroupRefId { get; set; }

    }
}