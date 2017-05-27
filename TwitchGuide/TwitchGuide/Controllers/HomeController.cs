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
using PagedList;

namespace TwitchGuide.Controllers
{
    public class HomeController : BaseController
    {

        private TwitchContext db = new TwitchContext();

        public ActionResult Index()
        {
            ViewBag.CurrentUsersUserName = "TylerConnors";
            ViewBag.CurrentUsersAvatar = "https://static-cdn.jtvnw.net/jtv_user_pictures/tconnorsbitsmith-profile_image-91feb60affd61ad5-300x300.png";
            var sorted = db.SiteNews.OrderByDescending((s => s.NewsID)).ToList();
            ViewData["MyData"] = sorted;
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

        public ActionResult AllUsers(int? page)
        {
            //sorting
            var sorted = db.Users.OrderBy((s => s.Username)).ToList();
            //paging
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(sorted.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult AboutUs()
        {
            return View();
        }

    }
}