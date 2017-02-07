﻿using ClassProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClassProject.Controllers
{
    public class HomeController : Controller
    {
        private NewsContext db = new NewsContext();

        // GET: Home
        public ActionResult Index(bool AscOrder = false)
        {
            if (AscOrder)
                return View(db.Stories.ToList().OrderBy(p => p.Timestamp));
            else
                return View(db.Stories.ToList().OrderByDescending(p => p.Timestamp));
        }
    }
}