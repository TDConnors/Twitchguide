using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using TwitchGuide.DAL;
using TwitchGuide.Models;
namespace TwitchGuide.Controllers
{
    public class HomeController : Controller
    {
        private TwitchContext db = new TwitchContext();

        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Test()
        {
            return View();
        }

        public ActionResult AllUsers()
        {
            return View(db.Users.ToList());
        }
    }
}