using SurveySystem.Models;
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

        public ActionResult Survey()
        {
            return View();
        }


        public ActionResult Start(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View();
        }
    }
}