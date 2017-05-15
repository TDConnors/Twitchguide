using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using TwitchGuide.DAL;
using TwitchGuide.Models;
using System.Net.Http;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using Microsoft.AspNet.Identity.Owin;

namespace TwitchGuide.Controllers
{
    public class userAndNews
    {
        public IEnumerable<SiteNews> siteNews { get; set; }
        public User user { get; set; }
    }
    public class HomeController : Controller
    {
        private TwitchContext db = new TwitchContext();

        public ActionResult Index()
        {
            List<SiteNews> siteNews = db.SiteNews.ToList();
            ViewData["MyData"] = siteNews;
            if (User.Identity.IsAuthenticated)
            {
                
                //Get the AuthToken for the current user, store in a ViewBag
                var UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                ApplicationUser currentUser = UserManager.FindById(User.Identity.GetUserId());
                var ourUser = db.Users.Where(p => p.Username == currentUser.UserName).FirstOrDefault();
                ViewBag.token = ourUser.AuthToken;

                return View(ourUser);
            }

            return View();
        }

        [HttpGet]
        public ActionResult LoginSuccess()
        {
            return View();
        }

        public ActionResult Search()
        {
            return View();
        }

        public ActionResult AllUsers()
        {
            return View(db.Users.ToList());
        }
        public ActionResult UserLink()
        {
            return View();
        }
        public ActionResult goToUser()
        {
            return View();
        }
    }
}