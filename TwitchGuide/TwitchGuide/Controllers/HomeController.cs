using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;

namespace TwitchGuide.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult LoginSuccess()
        {
            string code = this.Request.QueryString["code"];
            ViewBag.code = code;
            return View();
        }

    }
}