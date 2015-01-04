using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SurveySystem.Models;

namespace SurveySystem.Controllers
{
    public class QuestionGroupsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: QuestionGroups
        public ActionResult Index()
        {
            return View(db.ApplicationQuestionGroups.ToList());
        }

        // GET: QuestionGroups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionGroup questionGroup = db.ApplicationQuestionGroups.Find(id);
            if (questionGroup == null)
            {
                return HttpNotFound();
            }
            return View(questionGroup);
        }

        // GET: QuestionGroups/Create
        public ActionResult Create()
        {
            
            return View();
        }

        // POST: QuestionGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title")] QuestionGroup questionGroup)
        {
            if (ModelState.IsValid)
            {
                db.ApplicationQuestionGroups.Add(questionGroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(questionGroup);
        }

        // GET: QuestionGroups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionGroup questionGroup = db.ApplicationQuestionGroups.Find(id);
            if (questionGroup == null)
            {
                return HttpNotFound();
            }
            return View(questionGroup);
        }

        // POST: QuestionGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title")] QuestionGroup questionGroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(questionGroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(questionGroup);
        }

        // GET: QuestionGroups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionGroup questionGroup = db.ApplicationQuestionGroups.Find(id);
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
            QuestionGroup questionGroup = db.ApplicationQuestionGroups.Find(id);
            db.ApplicationQuestionGroups.Remove(questionGroup);
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
