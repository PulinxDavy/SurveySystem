using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using SurveySystem.Models;
using SurveySystem.ViewModels;

namespace SurveySystem.Controllers
{
    public class QuestionController : BaseController
    {
        // GET: Question
        public ActionResult Index()
        {
            List<QuestionVM> list = new List<QuestionVM>();
            foreach (Question q in Db.ApplicationQuestions.ToList())
            {
                QuestionVM question = new QuestionVM(q);
                list.Add(question);
            }
            return View(list);
        }

        // GET: Question/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = Db.ApplicationQuestions.Find(id);
            QuestionVM q = new QuestionVM(question);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(q);
        }

        // GET: Question/Create
        public ActionResult Create()
        {

            QuestionVM question = new QuestionVM();

            return View(question);
        }

        // POST: Question/Create
        [HttpPost]
        public ActionResult Create(QuestionVM question)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Question q;

                    if (question.Question.Type == "Very bad - Very good")
                    {
                        q = new Appreciation();
                    }
                    else if (question.Question.Type == "Agreement")
                    {
                        q = new Agreement();
                    }
                    else if (question.Question.Type == "Numerical")
                    {
                        q = new Numerical();
                    }
                    else if (question.Question.Type == "MultipleValues")
                    {
                        q = new MultipleValuesQ();
                    }
                    else if (question.Question.Type == "OpenEnd")
                    {
                        q = new OpenEndQ();
                    }
                    else if (question.Question.Type == "PreDefined")
                    {
                        q = new PreDefinedQ();
                    }
                    else if (question.Question.Type == "Selection")
                    {
                        q = new SelectionQ();
                    }
                    else
                    {
                        q = null;
                    }
                    if (q == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }

                    Db.ApplicationQuestions.Add(q);
                    Db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View(question);
            }
        }

        // GET: Question/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = Db.ApplicationQuestions.Find(id);
            QuestionVM q = new QuestionVM(question);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(q);
        }

        // POST: Question/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, QuestionVM question)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Db.Entry(question.Question).State = EntityState.Modified;
                    Db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View(question);
            }
        }

        // GET: Question/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = Db.ApplicationQuestions.Find(id);
            QuestionVM q = new QuestionVM(question);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(q);
        }

        // POST: Question/Delete/5
        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            Question question = Db.ApplicationQuestions.Find(id);
            Db.ApplicationQuestions.Remove(question);
            Db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
