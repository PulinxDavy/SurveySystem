using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SurveySystem.Models;
using SurveySystem.ViewModels;

namespace SurveySystem.Controllers
{
    public class SurveyResultsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SurveyResults
        public ActionResult Index()
        {
           
            List<SurveyResult> surveyResults = db.SurveyResults.ToList();
            List<int> surveyIds = new List<int>();

            foreach (SurveyResult s in surveyResults)
            {
                surveyIds.Add(s.SurveyId);
            }
            surveyIds.Distinct();

            List<Survey> surveys = new List<Survey>();
            SurveyResultVM surveyResultVM = new SurveyResultVM();

            foreach (int i in surveyIds)
            {
                surveys.Add(db.ApplicationSurveys.Find(i));
            }

            Survey survey = new Survey();
            survey.Title = "test";
            Appreciation app = new Appreciation();
            app.QuestionString = "test Qstring";
            survey.Questions.Add(app);
            surveys.Add(survey);
            surveyResultVM.Surveys = surveys;
            
            return View(surveyResultVM);
        }

        // GET: SurveyResults/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SurveyResult surveyResult = db.SurveyResults.Find(id);
            if (surveyResult == null)
            {
                return HttpNotFound();
            }
            return View(surveyResult);
        }

        // GET: SurveyResults/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SurveyResults/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SurveyId,UserId,QuestionId")] SurveyResult surveyResult)
        {
            if (ModelState.IsValid)
            {
                db.SurveyResults.Add(surveyResult);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(surveyResult);
        }

        // GET: SurveyResults/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SurveyResult surveyResult = db.SurveyResults.Find(id);
            if (surveyResult == null)
            {
                return HttpNotFound();
            }
            return View(surveyResult);
        }

        // POST: SurveyResults/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SurveyId,UserId,QuestionId")] SurveyResult surveyResult)
        {
            if (ModelState.IsValid)
            {
                db.Entry(surveyResult).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(surveyResult);
        }

        // GET: SurveyResults/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SurveyResult surveyResult = db.SurveyResults.Find(id);
            if (surveyResult == null)
            {
                return HttpNotFound();
            }
            return View(surveyResult);
        }

        // POST: SurveyResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SurveyResult surveyResult = db.SurveyResults.Find(id);
            db.SurveyResults.Remove(surveyResult);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: SurveyResults/Details/5
        public ActionResult Results(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SurveyResultStatisticsVM surveyResStat = new SurveyResultStatisticsVM();
            //List of all surveyResults of a given survey
            List<SurveyResult> surveyResults = db.SurveyResults.Where(res =>res.SurveyId == id).ToList();
            List<Question> questions = new List<Question>();
            //All answers for all questions of the survey --> do counts on this
            var answers = new List<Tuple<int,string>>();
            var countAnswers = new HashSet<Tuple<int, string, int>>();

            //We need a list of unique question Ids from the surveyResults
            HashSet<int> uniqueQuestionIds = new HashSet<int>();

            //get the questionIds and answers
            foreach (SurveyResult s in surveyResults)
            {
                foreach (string answer in s.Answers)
                {
                    answers.Add(Tuple.Create(s.QuestionId, answer));
                }
                   
                    uniqueQuestionIds.Add(s.QuestionId);   
            }

            foreach (var a in answers)
            {
               int count= answers.Where(x => x.Item1 == a.Item1 && x.Item2 == a.Item2).Count();
               countAnswers.Add(Tuple.Create(a.Item1, a.Item2, count));
            }

          
           
            //Add the questions
            foreach (int i in uniqueQuestionIds)
            {
                questions.Add(db.ApplicationQuestions.Find(i));
            }
            var uniqueAnswersPerQuestion = answers.Distinct().ToList();
            surveyResStat.SurveyQuestions = questions;
            surveyResStat.Answers=answers;
            surveyResStat.countAnswers = countAnswers;
            surveyResStat.uniqueAnswersPerQuestion = uniqueAnswersPerQuestion;

            return View(surveyResStat);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
