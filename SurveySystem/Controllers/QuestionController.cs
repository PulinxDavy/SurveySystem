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
            if (q == null)
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
                    questionVm.Answer = "Very bad;Bad;Good;Very good";
                }
                else if (questionVm.TypeOfAppreciation == "Agreement")
                {
                    q = new Agreement();
                    questionVm.Answer = "Agree;Don't agree;No opinion";
                }
                else if (questionVm.TypeOfAppreciation == "Numerical")
                {
                    q = new Numerical();
                    int counter = 10;
                    string createString = null;
                    for (int i = 1; i <= counter; i++)
                    {
                        createString = createString + i.ToString() + ";";
                    }
                    questionVm.Answer = createString;
                }
                else if (questionVm.TypeOfQuestion == "MultipleValues")
                {
                    MultipleValuesQ mQ = new MultipleValuesQ();
                    q = mQ;
                }
                else if (questionVm.TypeOfQuestion == "OpenEnd")
                {
                    q = new OpenEndQ();
                }
                else if (questionVm.TypeOfQuestion == "PreDefined")
                {
                    PreDefinedQ pQ = new PreDefinedQ();
                    q = pQ;
                }
                else if (questionVm.TypeOfQuestion == "Selection")
                {
                    SelectionQ sQ = new SelectionQ();
                    q = sQ;
                }
                else
                {
                    q = null;
                }
                if (q == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                
                q.QuestionString = questionVm.QuestionString;
                q.Answer = questionVm.Answer;

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
                        }
                        else if (questionVm.TypeOfAppreciation == "Very bad - Very good")
                        {
                            Db.ApplicationQuestions.Remove(q);
                            Db.SaveChanges();
                            Appreciation aQ = new Appreciation();
                            aQ.QuestionString = questionVm.QuestionString;
                            aQ.Answer = "Very bad;Bad;Good;Very Good";
                            q = aQ;
                        }
                        else if (questionVm.TypeOfAppreciation == "Agreement")
                        {
                            Db.ApplicationQuestions.Remove(q);
                            Db.SaveChanges();
                            Agreement aQ = new Agreement();
                            aQ.QuestionString = questionVm.QuestionString;
                            aQ.Answer = "Agree;Don't agree;No opinion";
                            q = aQ;
                        }
                        else if (questionVm.TypeOfAppreciation == "Numerical")
                        {
                            Db.ApplicationQuestions.Remove(q);
                            Db.SaveChanges();
                            Numerical nQ = new Numerical();
                            nQ.QuestionString = questionVm.QuestionString;
                            int counter = 10;
                            string createString = null;
                            for (int i = 1; i < counter; i++)
                            {
                                createString = createString + i.ToString() + ";";
                            }
                            nQ.Answer = createString;
                            q = nQ;
                        }
                    } else
                    {
                        q.QuestionString = questionVm.QuestionString;
                        q.Answer = questionVm.Answer;
                    }
                }
                else if (questionVm.TypeOfAppreciation == "Very bad - Very good" && questionVm.TypeOfQuestion == "Appreciation")
                {
                    Db.ApplicationQuestions.Remove(q);
                    Db.SaveChanges();
                    q = new Appreciation();
                    q.QuestionString = questionVm.QuestionString;

                }
                else if (questionVm.TypeOfAppreciation == "Agreement" && questionVm.TypeOfQuestion == "Appreciation")
                {
                    Db.ApplicationQuestions.Remove(q);
                    Db.SaveChanges();
                    q = new Agreement();
                    q.QuestionString = questionVm.QuestionString;
                }
                else if (questionVm.TypeOfAppreciation == "Numerical" && questionVm.TypeOfQuestion == "Appreciation")
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
                    q.Answer = questionVm.Answer;
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
                    q.Answer = questionVm.Answer;
                }
                else if (questionVm.TypeOfQuestion == "Selection")
                {
                    Db.ApplicationQuestions.Remove(q);
                    Db.SaveChanges();
                    q = new SelectionQ();
                    q.QuestionString = questionVm.QuestionString;
                    q.Answer = questionVm.Answer;
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

        public QuestionVM CreateQuestionVM(Question q)
        {
            QuestionVM question = new QuestionVM();
            question.Id = q.Id;
            question.QuestionString = q.QuestionString;
            question.TypeOfQuestion = q.Type;
            question.TypeOfAppreciation = q.AppreciationType;
            question.Answer = q.Answer;

            return question;
        }
    }
}
