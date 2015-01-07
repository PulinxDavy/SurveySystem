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
                QuestionVM question = CreateQuestionVM(q);
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
            QuestionVM q = CreateQuestionVM(question);
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
        public ActionResult Create(QuestionVM questionVm)
        {
            if (ModelState.IsValid)
            {
                Question q;

                if (questionVm.TypeOfAppreciation == "Very bad - Very good")
                {
                    q = new Appreciation();
                }
                else if (questionVm.TypeOfAppreciation == "Agreement")
                {
                    q = new Agreement();
                }
                else if (questionVm.TypeOfAppreciation == "Numerical")
                {
                    q = new Numerical();
                }
                else if (questionVm.TypeOfQuestion == "MultipleValues")
                {
                    q = new MultipleValuesQ();
                }
                else if (questionVm.TypeOfQuestion == "OpenEnd")
                {
                    q = new OpenEndQ();
                }
                else if (questionVm.TypeOfQuestion == "PreDefined")
                {
                    q = new PreDefinedQ();
                }
                else if (questionVm.TypeOfQuestion == "Selection")
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
                
                SplitString(questionVm, q);
                q.QuestionString = questionVm.QuestionString;

                Db.ApplicationQuestions.Add(q);
                Db.SaveChanges();
                

                return RedirectToAction("Index");
            }
            
            return View(questionVm);
        }

        // GET: Question/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = Db.ApplicationQuestions.Find(id);
            QuestionVM q = CreateQuestionVM(question);
            return View(q);
        }

        // POST: Question/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id, QuestionString, Answer, TypeOfQuestion, TypeOfAppreciation")] QuestionVM questionVm)
        {
            if (ModelState.IsValid)
            {
                Question q = Db.ApplicationQuestions.Find(questionVm.Id);
                if (q.Type == questionVm.TypeOfQuestion)
                {
                    if (q.Type == "Appreciation")
                    {
                        if (q.AppreciationType == questionVm.TypeOfAppreciation)
                        {
                            q.QuestionString = questionVm.QuestionString;
                            SplitString(questionVm, q);
                        }
                        else if (questionVm.TypeOfAppreciation == "Very bad - Very good")
                        {
                            Db.ApplicationQuestions.Remove(q);
                            Db.SaveChanges();
                            q = new Appreciation();
                            q.QuestionString = questionVm.QuestionString;
                        }
                        else if (questionVm.TypeOfAppreciation == "Agreement")
                        {
                            Db.ApplicationQuestions.Remove(q);
                            Db.SaveChanges();
                            q = new Agreement();
                            q.QuestionString = questionVm.QuestionString;
                        }
                        else if (questionVm.TypeOfAppreciation == "Numerical")
                        {
                            Db.ApplicationQuestions.Remove(q);
                            Db.SaveChanges();
                            q = new Numerical();
                            q.QuestionString = questionVm.QuestionString;
                        }
                    } else
                    {
                        q.QuestionString = questionVm.QuestionString;
                        SplitString(questionVm, q);
                    }
                }
                else if (questionVm.TypeOfAppreciation == "Very bad - Very good")
                {
                    Db.ApplicationQuestions.Remove(q);
                    Db.SaveChanges();
                    q = new Appreciation();
                    q.QuestionString = questionVm.QuestionString;

                }
                else if (questionVm.TypeOfAppreciation == "Agreement")
                {
                    Db.ApplicationQuestions.Remove(q);
                    Db.SaveChanges();
                    q = new Agreement();
                    q.QuestionString = questionVm.QuestionString;
                }
                else if (questionVm.TypeOfAppreciation == "Numerical")
                {
                    Db.ApplicationQuestions.Remove(q);
                    Db.SaveChanges();
                    q = new Numerical();
                    q.QuestionString = questionVm.QuestionString;
                }
                else if (questionVm.TypeOfQuestion == "MultipleValues")
                {
                    Db.ApplicationQuestions.Remove(q);
                    Db.SaveChanges();
                    q = new MultipleValuesQ();
                    q.QuestionString = questionVm.QuestionString;
                    SplitString(questionVm, q);
                }
                else if (questionVm.TypeOfQuestion == "OpenEnd")
                {
                    Db.ApplicationQuestions.Remove(q);
                    Db.SaveChanges();
                    q = new OpenEndQ();
                    q.QuestionString = questionVm.QuestionString;
                }
                else if (questionVm.TypeOfQuestion == "PreDefined")
                {
                    Db.ApplicationQuestions.Remove(q);
                    Db.SaveChanges();
                    q = new PreDefinedQ();
                    q.QuestionString = questionVm.QuestionString;
                    SplitString(questionVm, q);
                }
                else if (questionVm.TypeOfQuestion == "Selection")
                {
                    Db.ApplicationQuestions.Remove(q);
                    Db.SaveChanges();
                    q = new SelectionQ();
                    q.QuestionString = questionVm.QuestionString;
                    SplitString(questionVm, q);
                }

                if (q.Id == questionVm.Id)
                {
                    Db.Entry(q).State = EntityState.Modified;
                }
                else
                {
                    Db.ApplicationQuestions.Add(q);
                }
                
                Db.SaveChanges();

                return RedirectToAction("Index");
            }
            
            return View(questionVm);
 
        }

        // GET: Question/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = Db.ApplicationQuestions.Find(id);
            QuestionVM q = CreateQuestionVM(question);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(q);
        }

        // POST: Question/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Question question = Db.ApplicationQuestions.Find(id);
            Db.ApplicationQuestions.Remove(question);
            Db.SaveChanges();
            return RedirectToAction("Index");
        }

        public void SplitString(QuestionVM question, Question q)
        {
            if (question.Answer != null)
            {
                char delimiterChar = ';';

                string[] splitString = question.Answer.Split(delimiterChar);

                foreach (string a in splitString)
                {
                    q.Answer.Add(a);
                }
            }
        }

        public void CreateString(Question question, QuestionVM q)
        {
            if (question.Answer != null)
            {
                string createString = null;
                foreach (string str in question.Answer)
                {
                    createString = createString + str + ";";
                }

                q.Answer = createString;
            }
        }

        public QuestionVM CreateQuestionVM(Question q)
        {
            QuestionVM question = new QuestionVM();
            question.Id = q.Id;
            question.QuestionString = q.QuestionString;
            question.TypeOfQuestion = q.Type;
            question.TypeOfAppreciation = q.AppreciationType;
            CreateString(q, question);

            return question;
        }
    }
}
