﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TwitchGuide
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "UserSearch",
                url: "Users/{name}",
                defaults: new { controller = "Profile", action = "Search", name = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "signin-Twitch",
                url: "signin-Twitch",
                defaults: new {controller = "[AccountController]", action = "ExternalLoginCallback", code = UrlParameter.Optional }
            );
			
			routes.MapRoute(
                name: "UserGet",
                url: "Users/{name}",
                defaults: new { controller = "Home", action = "goToUser", name = UrlParameter.Optional }
            );
			
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );


        }
    }
}
