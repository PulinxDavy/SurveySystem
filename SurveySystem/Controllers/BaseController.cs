using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SurveySystem.Models;

namespace SurveySystem.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        protected ApplicationDbContext Db = new ApplicationDbContext();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}