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
           // questionGroupShowVM.CurrentQuestionGroup = survey.QuestionGroups.First();
            surveyShowVM.QuestionGroupShowVM = questionGroupShowVM;
            surveyShowVM.Survey = survey;
            return View(surveyShowVM);
        }


       public ActionResult Next(SurveyShowVM surveyShowVM)
        {
            if (ModelState.IsValid)
            {
                int index = surveyShowVM.QuestionGroupShowVM.CurrentQuestionGroupIndex;
                if ( index < surveyShowVM.QuestionGroupShowVM.QuestionGroups.Count())
                {
                    index++;
                    surveyShowVM.QuestionGroupShowVM.CurrentQuestionGroupIndex = index;

                    foreach (Question q in surveyShowVM.QuestionGroupShowVM.CurrentQuestionGroup.Questions)
                    {
                        SurveyResult surveyResult = new SurveyResult();
                        surveyResult.QuestionId = q.Id;
                        surveyResult.SurveyId = surveyShowVM.Survey.Id;
                        Answers answer = new Answers();
                        answer.Answer = q.Answer;

                        surveyResult.Answer = answer;

                        db.SurveyResults.Add(surveyResult);
                        db.SaveChanges();


                        surveyShowVM.QuestionGroupShowVM.CurrentQuestionGroup = surveyShowVM.QuestionGroupShowVM.QuestionGroups.ElementAt(index);

                        return View(surveyShowVM.QuestionGroupShowVM);
                    }
                    }else
                {
                    index = surveyShowVM.QuestionShowVM.CurrentQuestionIndex;

                    if(index < surveyShowVM.QuestionShowVM.Questions.Count()){
                        index++;
                        Question q = surveyShowVM.QuestionShowVM.CurrentQuestion;
                         SurveyResult surveyResult = new SurveyResult();
                        surveyResult.QuestionId = q.Id;
                        surveyResult.SurveyId = surveyShowVM.Survey.Id;
                        Answers answer = new Answers();
                        answer.Answer = q.Answer;

                        surveyResult.Answer = answer;

                        db.SurveyResults.Add(surveyResult);
                        db.SaveChanges();

                        surveyShowVM.QuestionShowVM.CurrentQuestion =surveyShowVM.QuestionShowVM.Questions.ElementAt(index);

                        return View(surveyShowVM.QuestionShowVM);
                    }else{
                        return RedirectToAction("Index");
                    }
                }
                
                }
           return View(surveyShowVM);
            }

        }
    }
