using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using TwitchGuide.Models;
using TwitchGuide.DAL;
using Microsoft.AspNet.Identity.Owin;

namespace TwitchGuide.Controllers
{
    public class HomeController : Controller
    {
        private TwitchContext db = new TwitchContext();

        public ActionResult Index()
        {

            //Get the AuthToken for the current user, store in a ViewBag
            var UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser currentUser = UserManager.FindById(User.Identity.GetUserId());
            var ourUser = db.Users.Where(p => p.Username == currentUser.UserName).FirstOrDefault();
            ViewBag.token = ourUser.AuthToken;

            return View();
        }

        [HttpGet]
        public ActionResult LoginSuccess()
        {

            if (ViewBag.code == null)
            {
                ViewBag.code = "no code because code = null";
                return View();
            }

            return View();
        }


    }
}