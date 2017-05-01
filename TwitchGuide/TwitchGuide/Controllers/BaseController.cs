using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TwitchGuide.Models;
using TwitchGuide.DAL;
using Microsoft.AspNet.Identity;

namespace TwitchGuide.Controllers
{
    public class BaseController : Controller
    {
        private TwitchContext db = new TwitchContext();

        public User GetUser()
        {
            string Username = (Session["UserId"] != null) ? Session["Username"].ToString() : User.Identity.GetUserName();

            var Data = from a in db.Users
                       where a.Username.Equals(Username)
                       select a;

            return Data.FirstOrDefault();
        }
        public bool isLoggedIn()
        {
            return User.Identity.IsAuthenticated;
        }
        public int getUserID()
        {
            //if (isLoggedIn())
            //{
            //   int myID = GetUser().UserID;
            //   return myID;
            //}
            //else
                return 7;
        }
    }
}