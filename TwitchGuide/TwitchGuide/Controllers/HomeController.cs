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
    public class HomeController : BaseController
    {
        private TwitchContext db = new TwitchContext();

        public ActionResult Index()
        {
            ViewData["MyData"] = db.SiteNews.ToList();
            if (isLoggedIn())
            {
                User ourUser = GetUser();
                return View(ourUser);
            }
            return View();
        }

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
        public ActionResult AboutUs()
        {
            return View();
        }

    }
}