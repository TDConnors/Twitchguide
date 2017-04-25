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
            string UserId = (Session["UserId"] != null) ? Session["UserId"].ToString() : User.Identity.GetUserId();

            var Data = from a in db.Users
                       where a.UserID.Equals(UserId)
                       select a;

            return Data.FirstOrDefault();
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
        public User getUserByID(int UserId)
        { 
            var Data = from a in db.Users
                       where a.UserID.Equals(UserId)
                       select a;
            return Data.FirstOrDefault();
        }
        public string getUsername(int UserID)
        {
            User tempUser = getUserByID(UserID);
            string username = tempUser.Username;
            return username;
        }
    }
}