using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SurveySystem.Models;
using SurveySystem.ViewModels;

namespace SurveySystem.Controllers
{
    public class SurveyController : BaseController
    {
        // GET: Survey
        public ActionResult Index()
        {
            var list = new List<SurveyVM>();

            foreach (var s in Db.ApplicationSurveys.ToList())
            {
                var survey = CreateSurveyVm(s);
                list.Add(survey);
            }

            return View(list);
        }

        // GET: Survey/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var survey = Db.ApplicationSurveys.Find(id);
            var s = CreateSurveyVm(survey);
            if (s == null)
            {
                return HttpNotFound();
            }
            return View(s);
        }

        // GET: Survey/Create
        public ActionResult Create()
        {

            var survey = new SurveyVM();
            survey.AllQuestions = Db.ApplicationQuestions.ToList();
            survey.AllGroups = Db.QuestionGroups.ToList();

            return View(survey);
        }

        // POST: Survey/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title, Description, Active, Comment, SelectedGroups, SelectedQuestions")]SurveyVM s)
        {
            if (ModelState.IsValid)
            {
                var survey = new Survey();
                survey.Title = s.Title;
                survey.Description = s.Description;
                survey.Active = s.Active;
                if (survey.Active)
                {
                    survey.ActiveSince = DateTime.Today;
                }
                survey.Comment = s.Comment;
                foreach (var item in s.SelectedGroups)
                {
                    var g = Db.QuestionGroups.Find(item);
                    survey.QuestionGroups.Add(g);
                }
                foreach (var item in s.SelectedQuestions)
                {
                    var q = Db.ApplicationQuestions.Find(item);
                    survey.Questions.Add(q);
                }

                Db.ApplicationSurveys.Add(survey);
                Db.SaveChanges();

                return RedirectToAction("Index");
            }
                
            return View(s);
            
        }

        // GET: Survey/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var survey = Db.ApplicationSurveys.Find(id);
            var surveyEdit = new SurveyEditVM();
            surveyEdit.Survey = survey;
            surveyEdit.AllGroups = Db.QuestionGroups.ToList();
            surveyEdit.AllQuestions = Db.ApplicationQuestions.ToList();
            return View(surveyEdit);
        }

        // POST: Survey/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Survey, SelectedGroups, SelectedQuestions")]SurveyEditVM surveyEdit)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var survey = Db.ApplicationSurveys.Find(surveyEdit.Survey.Id);
                    survey.Title = surveyEdit.Survey.Title;
                    survey.Description = surveyEdit.Survey.Description;
                    survey.Active = surveyEdit.Survey.Active;
                    if (survey.Active)
                    {
                        survey.ActiveSince = DateTime.Today;
                    }
                    foreach (var item in surveyEdit.SelectedGroups)
                    {
                        var g = Db.QuestionGroups.Find(item);
                        survey.QuestionGroups.Add(g);
                    }
                    foreach (var item in surveyEdit.SelectedQuestions)
                    {
                        var q = Db.ApplicationQuestions.Find(item);
                        survey.Questions.Add(q);
                    }

                    Db.Entry(survey).State = EntityState.Modified;
                    Db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Survey/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var survey = Db.ApplicationSurveys.Find(id);
            var s = CreateSurveyVm(survey);
            if (s == null)
            {
                return HttpNotFound();
            }
            return View(s);
        }

        // POST: Survey/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var survey = Db.ApplicationSurveys.Find(id);
            Db.ApplicationSurveys.Remove(survey);
            Db.SaveChanges();
            return RedirectToAction("Index");
        }

        public SurveyVM CreateSurveyVm(Survey s)
        {
            SurveyVM survey = new SurveyVM();
            survey.Id = s.Id;
            survey.Title = s.Title;
            survey.Description = s.Description;
            survey.Active = s.Active;
            survey.Comment = s.Comment;
            survey.AllGroups = s.QuestionGroups.Cast<QuestionGroup>().ToList();
            survey.AllQuestions = s.Questions.Cast<Question>().ToList();

            return survey;
        }
    }
}
