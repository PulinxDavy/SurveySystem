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
using System.Diagnostics;

namespace SurveySystem.Controllers
{
    public class QuestionGroupsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: QuestionGroups
        public ActionResult Index()
        {
            return View(db.QuestionGroups.ToList());
        }

        // GET: QuestionGroups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionGroup questionGroup = db.QuestionGroups.Find(id);
            if (questionGroup == null)
            {
                return HttpNotFound();
            }
            return View(questionGroup);
        }

        // GET: QuestionGroups/Create
        public ActionResult Create()
        {
            QuestionGroupVM questionGroupVM = new QuestionGroupVM();
       //     questionGroupVM.AllQuestions = new List<Question>();
            questionGroupVM.AllQuestions = db.ApplicationQuestions.ToList();
           
           
            return View(questionGroupVM);
        }

        // POST: QuestionGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(QuestionGroupVM questionGroupVM)
        {
            if (ModelState.IsValid)
            {
                QuestionGroup questionGroup = new QuestionGroup();
         
                foreach(int i in questionGroupVM.SelectedQuestions){
           
                   var q= db.ApplicationQuestions.Find(i);             
                   questionGroup.Questions.Add(q);
                 }
                questionGroup.Title = questionGroupVM.Title;
                db.QuestionGroups.Add(questionGroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(questionGroupVM);
        }

        // GET: QuestionGroups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EditQuestionGroupVM editQuestionGroup = new EditQuestionGroupVM();
            QuestionGroup questionGroup = db.QuestionGroups.Find(id);
            editQuestionGroup.AllQuestions = db.ApplicationQuestions.ToList();
            editQuestionGroup.SelectedQuestionGroup = questionGroup;
            if (questionGroup == null)
            {
                return HttpNotFound();
            }
            return View(editQuestionGroup);
        }

        // POST: QuestionGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SelectedQuestionGroup, SelectedQuestions")] EditQuestionGroupVM editQuestionGroup)
        {
            if (ModelState.IsValid)
            {
                var questionGroup = db.QuestionGroups.Find(editQuestionGroup.SelectedQuestionGroup.Id);
                questionGroup.Title = editQuestionGroup.SelectedQuestionGroup.Title;
                questionGroup.Questions.Clear();

                foreach (int i in editQuestionGroup.SelectedQuestions)
                {

                    var q = db.ApplicationQuestions.Find(i);
                    questionGroup.Questions.Add(q);
                }
                db.Entry(questionGroup).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(editQuestionGroup);
        }

        // GET: QuestionGroups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionGroup questionGroup = db.QuestionGroups.Find(id);
            if (questionGroup == null)
            {
                return HttpNotFound();
            }
            return View(questionGroup);
        }

        // POST: QuestionGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QuestionGroup questionGroup = db.QuestionGroups.Find(id);
            db.QuestionGroups.Remove(questionGroup);
            db.SaveChanges();
            return RedirectToAction("Index");
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
