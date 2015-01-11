using SurveySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return View();
        }

        public ActionResult Survey()
        {
            return View();
        }
    }
}