﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TwitchGuide.Models;
using TwitchGuide.DAL;

namespace TwitchGuide
{
    public class CurrentUserProfile : ActionFilterAttribute 
    {
        private TwitchContext db = new TwitchContext();
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                string temp = HttpContext.Current.User.Identity.Name;
                User ourUser = db.Users.Where(p => p.Username == temp).FirstOrDefault();
                filterContext.Controller.ViewBag.CurrentUsersUserName = ourUser.Username;
                filterContext.Controller.ViewBag.CurrentUsersAvatar =  ourUser.Avatar;
                filterContext.Controller.ViewBag.CurrentUsersTwitchPage = "https://www.twitch.tv/"+ ourUser.Username.ToLower();
            }
        }

    }
    
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalFilters.Filters.Add(new CurrentUserProfile(), 0);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
