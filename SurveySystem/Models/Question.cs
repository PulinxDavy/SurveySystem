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

        public string Answer { get; set; }
        public List<string> PossibleAnswers { get; set; }

        public abstract string Type { get; }

        public abstract string AppreciationType { get; }

        public string QuestionString { get; set; }

        public virtual ICollection<QuestionGroup> QuestionGroups { get; set; }

        public virtual ICollection<Survey> Surveys { get; set; }

        public Question()
        {
            this.QuestionGroups = new List<QuestionGroup>();
            this.Surveys = new List<Survey>();
            if (Answer != null)
                this.PossibleAnswers = SplitString(Answer);
        }

        public List<string> SplitString(string str)
        {
            List<string> list = new List<string>();
            char delimiter = Convert.ToChar(";");
            string[] splitStrings = str.Split(delimiter);
            foreach (var s in splitStrings)
            {
                list.Add(s);
            }

            return list;
        } 
    }
}