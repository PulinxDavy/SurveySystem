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
            qG.CurrentQuestionGroupIndex = 1;
            return View(qG);
        }

        public ActionResult Group(int id, int index)
        {

            return View();
        }

        [ValidateAntiForgeryToken]
        public ActionResult Group([Bind(Include = "Survey, QuestionGroup, CurrentQuestionGroupIndex, OtherAnswer")]QuestionGroupShowVM questionGroupShow)
        {
            if (ModelState.IsValid)
            {
                foreach (Question q in questionGroupShow.QuestionGroup.Questions)
                {
                    Answers answer = 
                }
            }
            return View();
        }

        public ActionResult Question(int id)
        {
            
            return View();
        }
    }
}
