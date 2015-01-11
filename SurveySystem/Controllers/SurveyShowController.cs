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
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SurveyShow
        public ActionResult Index()
        {
            List<Survey> surveys = db.ApplicationSurveys.Where(x => x.Active == true).ToList();
            return View(surveys);
        }

        public ActionResult Survey(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Survey survey = db.ApplicationSurveys.Find(id);
            SurveyShowVM surveyShowVM = new SurveyShowVM();
            QuestionGroupShowVM questionGroupShowVM = new QuestionGroupShowVM();
            //Just for testing. Logic needed to keep track of which QuestionGroup currently needs rendering
          //  questionGroupShowVM.CurrentQuestionGroup = survey.QuestionGroups.First();
            surveyShowVM.QuestionGroupShowVM = questionGroupShowVM;
            surveyShowVM.Survey = survey;
            return View(surveyShowVM);
        }


     /*   public ActionResult Start(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View();
        }*/
    }
}