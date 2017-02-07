using ClassProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClassProject.Controllers
{
    public class HomeController : Controller
    {
        private NewsContext db = new NewsContext();

        // GET: Home
        public ActionResult Index()
        {
            return View(db.Stories.ToList());
        }
    }
}