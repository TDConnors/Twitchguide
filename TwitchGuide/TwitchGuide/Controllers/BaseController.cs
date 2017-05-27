using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TwitchGuide.Models;
using TwitchGuide.DAL;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using Microsoft.AspNet.Identity.Owin;

namespace TwitchGuide.Controllers
{
    public class BaseController : Controller
    {
        private TwitchContext db = new TwitchContext();

        protected override ViewResult View(IView view, object model)
        {
            if (ViewBag.CurrentUsersUserName == "")
            {
                if (isLoggedIn())
                {
                    this.ViewBag.CurrentUsersUserName = GetUser().Username;
                    this.ViewBag.CurrentUsersAvatar = GetUser().Avatar;
                }
                else
                {
                    this.ViewBag.CurrentUsersUserName = "";
                    this.ViewBag.CurrentUsersAvatar = "";
                }
            }
            return base.View(view, model);
        }

        public User GetUser()
        {
            var UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser currentUser = UserManager.FindById(User.Identity.GetUserId());
            var ourUser = db.Users.Where(p => p.Username == currentUser.UserName).FirstOrDefault();
            return ourUser;
        }
        public bool isLoggedIn()
        {
            return User.Identity.IsAuthenticated;
        }
        public int getUserID()
        {
            if (isLoggedIn())
            {
               int myID = GetUser().UserID;
               return myID;
            }
            else
                return -1;
        }
    }
}