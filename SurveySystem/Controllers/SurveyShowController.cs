using SurveySystem.Models;
using SurveySystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SurveySystem.Controllers
{
    public class SurveyShowController : Controller
    {
        private ApplicationDbContext Db = new ApplicationDbContext();

        // GET: SurveyShow
        public ActionResult Index()
        {
            List<Survey> surveys = Db.ApplicationSurveys.Where(x => x.Active == true).ToList();
            return View(surveys);
        }

        public ActionResult Survey(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Survey survey = Db.ApplicationSurveys.Find(id);
            SurveyShowVM surveyShowVM = new SurveyShowVM();
            QuestionGroupShowVM questionGroupShowVM = new QuestionGroupShowVM();
            //Just for testing. Logic needed to keep track of which QuestionGroup currently needs rendering
            questionGroupShowVM.QuestionGroup = survey.QuestionGroups.First();
            surveyShowVM.QuestionGroupShowVM = questionGroupShowVM;
            surveyShowVM.Survey = survey;
            return View(surveyShowVM);
        }

        public ActionResult Group(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Survey survey = Db.ApplicationSurveys.Find(id);
            QuestionGroupShowVM qG = new QuestionGroupShowVM();
            qG.Survey = survey;
            if (survey.QuestionGroups == null || survey.QuestionGroups.Count == 0)
            {
                return RedirectToAction("Question");
            }
            qG.QuestionGroup = survey.QuestionGroups.First();
            if (qG.CurrentQuestionGroupIndex == 0)
            {
                qG.CurrentQuestionGroupIndex = 1;
            }
            else
            {
                qG.CurrentQuestionGroupIndex = qG.CurrentQuestionGroupIndex++;
            }
            return View(qG);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Group([Bind(Include = "Survey, QuestionGroup, CurrentQuestionGroupIndex, AnswersList")]QuestionGroupShowVM questionGroupShow)
        {
            if (ModelState.IsValid)
            {
                foreach (Tuple<int, string> t in questionGroupShow.AnswersList)
                {
                    SurveyResult surveyResult = new SurveyResult();
                    surveyResult.Answer.Answer = t.Item2;
                    surveyResult.SurveyId = questionGroupShow.Survey.Id;
                    surveyResult.QuestionId = t.Item1;

                    Db.SurveyResults.Add(surveyResult);
                    Db.SaveChanges();
                }

                NextGroup(questionGroupShow.CurrentQuestionGroupIndex, questionGroupShow.Survey);
            }
            return View(questionGroupShow);
        }

        public ActionResult Question(int id)
        {
            
            return View();
        }

        public void NextGroup(int index, Survey survey)
        {
            int count = 0;
            foreach (QuestionGroup qG in survey.QuestionGroups)
            {
                count++;
                if (index < count)
                {
                    Group(qG.Id);
                    break;
                }
            }
        }
    }
}
