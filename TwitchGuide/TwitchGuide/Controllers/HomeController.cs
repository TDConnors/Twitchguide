using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using Microsoft.AspNet.Identity;

namespace TwitchGuide.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
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