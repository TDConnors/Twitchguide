using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TwitchGuide.DAL;
using TwitchGuide.Models;

namespace TwitchGuide.Controllers
{
    public class ProfileController : Controller
    {
        private TwitchContext db = new TwitchContext();

        // Index
        public ActionResult Index(int? id)
        {
            var profileModel = new ProfileModel { };

            if (id == null)
            {
                //replace with current user
                var sched1 = db.Users.FirstOrDefault().Schedules.ToList();
                var tb1 = sched1.Join(db.Timeblocks,
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
                         }).ToList();

                profileModel = new ProfileModel
                {
                    TimeblockObj = tb1,
                    UserObj = db.Users.FirstOrDefault()
                            };
                return View(profileModel);
            }


            var uid = db.Users.Find(id);

            if (uid == null)
            {
                return HttpNotFound();
            }

            var tb = uid.Schedules.ToList().Join(db.Timeblocks,
                          p => p.TimeblockID,
                          e => e.Index,
                          (p, e) => new Timeblock {
                              Index = e.Index,
                              StartHour = e.StartHour,
                              StartMinute = e.StartMinute,
                              EndHour = e.EndHour,
                              EndMinute = e.EndMinute,
                              Day = e.Day }).ToList();


            profileModel = new ProfileModel
                            {
                                TimeblockObj = tb,
                                UserObj = db.Users.FirstOrDefault()
            };
            return View(profileModel);
        }


        //
        //CRUD Functionality Below
        //

        // GET: Profile/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Profile/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StartHour,StartMinute,EndHour,EndMinute,Day,Index")] Timeblock timeblock)
        {
            if (ModelState.IsValid)
            {
                db.Timeblocks.Add(timeblock);
                db.SaveChanges();
                //Replace with current user
                db.Schedules.Add(new Schedule { UserID = 1, TimeblockID = timeblock.Index });
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(timeblock);
        }


        // GET: Timeblocks/Delete/#
        public ActionResult Delete(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Timeblock timeblock = db.Timeblocks.Where(p => p.Index == id).FirstOrDefault();
            if (timeblock == null)
            {
                return HttpNotFound();
            }

            db.Users.FirstOrDefault().Schedules.Where(p => p.TimeblockID == id).FirstOrDefault();

            Schedule mapping = db.Users.FirstOrDefault().Schedules.Where(p => p.TimeblockID == id).FirstOrDefault();
            db.Schedules.Remove(mapping);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
