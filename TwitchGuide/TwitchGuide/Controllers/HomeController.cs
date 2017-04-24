using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using Microsoft.AspNet.Identity;
using System.Security.Claims;

namespace TwitchGuide.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var token = identity.Claims.Where(a => a.Type.Contains("twitch:access_token")).Last();
            ViewBag.token = token;
            return View(token);
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