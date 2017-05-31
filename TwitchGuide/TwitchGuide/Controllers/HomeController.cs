using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using TwitchGuide.DAL;
using TwitchGuide.Models;
using System.Net.Http;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using Microsoft.AspNet.Identity.Owin;
using PagedList;

namespace TwitchGuide.Controllers
{
    public class HomeController : BaseController
    {

        private TwitchContext db = new TwitchContext();

        public ActionResult Index()
        {
            var sorted = db.SiteNews.OrderByDescending((s => s.NewsID)).ToList();
            ViewData["MyData"] = sorted;
            if (isLoggedIn())
            {
                User ourUser = GetUser();
                var today = DateTime.Now;
                int m = today.Minute;
                int h = today.Hour;
                int d = (int)today.DayOfWeek;
                int minH = 99;
                int minM = 99;



                var favs = ourUser.Follows.ToArray();

                for (int i = 0; i < favs.Length; i++)
                {
                    var tbs = favs[i].Schedules.ToArray().Join(db.Timeblocks,
                          p => p.TimeblockID,
                          e => e.Index,
                          (p, e) => new Timeblock
                          {
                              Index = e.Index,
                              StartHour = e.StartHour,
                              StartMinute = e.StartMinute,
                              EndHour = e.EndHour,
                              EndMinute = e.EndMinute,
                              Day = e.Day
                          }).ToArray();

                    for (int k = 0; k < tbs.Length; k++)
                    {
                        if (tbs[k].Day == d) //get today's blocks
                        {
                            if (tbs[k].StartHour > h)
                            {
                                if (tbs[k].StartHour < minH)
                                {
                                    minH = tbs[k].StartHour;
                                    minM = tbs[k].StartMinute;
                                }
                            }
                            if (tbs[k].StartHour == h)
                            {
                                if (tbs[k].StartMinute > m)
                                {
                                    minH = tbs[k].StartHour;
                                    minM = tbs[k].StartMinute;
                                }
                            }
                        }
                    }
                }
                if (minH != 99)
                {
                    ViewBag.NextTime = "Today's next upcoming stream begins at " + minH + ":" + minM;
                }
                else
                    ViewBag.NextTime = "None of your favorites have upcoming streams today.";


                return View(ourUser);
            }
            return View();
        }

        public ActionResult LoginSuccess()
        {
            return View();
        }

        public ActionResult Search()
        {
            return View();
        }

        public ActionResult AllUsers(int? page)
        {
            //sorting
            var sorted = db.Users.OrderBy((s => s.Username)).ToList();
            //paging
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(sorted.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult AboutUs()
        {
            return View();
        }

    }
}