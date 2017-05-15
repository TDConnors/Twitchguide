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
    public class HomeController : BaseController
    {
        private TwitchContext db = new TwitchContext();

        public ActionResult Index()
        {
            List<SiteNews> siteNews = db.SiteNews.ToList();
            ViewData["MyData"] = siteNews;
			if(isLoggedIn())
            { 
				User ourUser = GetUser();
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
        public ActionResult AboutUs()
        {
            return View();
        }
        //public ActionResult AddAvatar(String logo)
        //{ 
        //    User current = GetUser();
        //    current.Avatar = logo;
        //    db.Entry(current).State = EntityState.Modified;
        //    db.SaveChanges();
        //    return Redirect(Request.UrlReferrer.ToString());
        //}
    }
}