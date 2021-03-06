﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TwitchGuide.DAL;
using TwitchGuide.Models;
using System.Threading.Tasks;

namespace TwitchGuide.Controllers
{
    public class ProfileController : BaseController
    {
        private TwitchContext db = new TwitchContext();

        // Index
        public ActionResult Search(string name = null)
        {
            var profileModel = new ProfileModel { };
            string username = "";
            ViewBag.edit = "False";
            ViewBag.PageUserName = "";
            ViewBag.PageUserLogo = "";

            if (name == null) //load current user's profile if logged in
            {

                if (isLoggedIn())
                {
                    User MyUser = GetUser();
                    username = MyUser.Username;
                    ViewBag.PageUserName = MyUser.Username;
                    ViewBag.PageUserLogo = MyUser.Avatar;
                    ViewBag.edit = "True";
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
                //get timeblocks
                var sched1 = db.Users.Where(p => p.Username == username).FirstOrDefault().Schedules.ToList();
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

                //compile
                profileModel = new ProfileModel
                {
                    TimeblockObj = tb1,
                    UserObj = db.Users.Where(p => p.Username == username).FirstOrDefault()
                };
                ViewBag.token = profileModel.UserObj.AuthToken;
                return View(profileModel);
            }

            //find other users

            var findUser = db.Users.Where(p => p.Username == name).FirstOrDefault();

            if (findUser == null)
            {
                return RedirectToAction("AdvancedSearch", new { name = name });
            }
            //get user schedule
            var tb = findUser.Schedules.ToList().Join(db.Timeblocks,
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

            ViewBag.PageUserName = findUser.Username;
            ViewBag.PageUserLogo = findUser.Avatar;
            profileModel = new ProfileModel
            {
                TimeblockObj = tb,
                UserObj = findUser
            };
            ViewBag.token = profileModel.UserObj.AuthToken;
            return View(profileModel);
        }

        public ActionResult AdvancedSearch(string name)
        {
            var names = db.Users.Where(p => p.Username.Contains(name)).ToList();
            if (names.Count < 1)
            {
                ViewBag.none = "True";
            }
            return View(names);
        }

        //
        //CRUD Functionality Below
        //

        // GET: Profile/Create
        [Authorize]
        public ActionResult Create()
        {
            return base.View();
        }

        // POST: Profile/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StartHour,StartMinute,EndHour,EndMinute,Day,Index")] Timeblock timeblock)
        {

            if (ModelState.IsValid)
            {
                if (timeblock.StartHour > 12 || timeblock.EndHour > 12)
                {
                    ViewBag.message = "Start Hour and End Hour must be between 1 and 12";
                    return View(timeblock);
                }

                //Adjust times to reflect AM/PM toggle
                if (Request["AM_Start"] != "on" && timeblock.StartHour != 12)
                {
                    timeblock.StartHour += 12;
                }
                if (Request["AM_End"] != "on" && timeblock.EndHour != 12)
                {
                    timeblock.EndHour += 12;
                }

                //make sure the start time is before the end time
                if (timeblock.StartHour > timeblock.EndHour)
                {
                    ViewBag.message = "The End Time cannot be before the Start Time.";
                    return View(timeblock);
                }
                if (timeblock.StartHour == timeblock.EndHour)
                {
                    if (timeblock.StartMinute >= timeblock.EndMinute)
                    {
                        ViewBag.message = "The End Time cannot be before the Start Time.";
                        return View(timeblock);
                    }
                    if ((timeblock.EndMinute - timeblock.StartMinute) < 15)
                    {
                        ViewBag.message = "The timeblock duration cannot be less than 15 minutes.";
                        return View(timeblock);
                    }
                }

                //Max timeblock length is 10 hours
                if ((timeblock.EndHour - timeblock.StartHour) > 9)
                {
                    ViewBag.message = "The timeblock duration cannot be greater than 9 hours";
                    return View(timeblock);
                }

                    //Check if timeblock already exists in DB
                    var check = db.Timeblocks.Where(p =>
                    (p.Day == timeblock.Day) &&
                    (p.StartHour == timeblock.StartHour) &&
                    (p.StartMinute == timeblock.StartMinute) &&
                    (p.EndHour == timeblock.EndHour) &&
                    (p.EndMinute == timeblock.EndMinute)).FirstOrDefault();

                if (check == null) //Timeblock doesn't exist yet
                {
                    db.Timeblocks.Add(timeblock);
                    db.SaveChanges();

                    var username = User.Identity.GetUserName();
                    var currentUser = db.Users.Where(p => p.Username == username).FirstOrDefault();
                    
                    db.Schedules.Add(new Schedule { UserID = currentUser.UserID, TimeblockID = timeblock.Index });
                    db.SaveChanges();
                    return RedirectToAction("Search");
                }
                else
                {
                    var username = User.Identity.GetUserName();
                    var currentUser = db.Users.Where(p => p.Username == username).FirstOrDefault();

                    //check if the user already has that timeblock
                    var check2 = db.Schedules.Where(p => (p.UserID == currentUser.UserID) && (p.TimeblockID == check.Index)).FirstOrDefault();

                    if (check2 == null) //User does not have the timeblock
                    {
                        db.Schedules.Add(new Schedule { UserID = currentUser.UserID, TimeblockID = check.Index });
                        db.SaveChanges();
                        return RedirectToAction("Search");
                    }
                }
            }
            return View(timeblock);
        }


        // GET: Timeblocks/Delete/#
        [Authorize]
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

            var username = User.Identity.GetUserName();
            var currentUser = db.Users.Where(p => p.Username == username).FirstOrDefault();

            Schedule mapping = currentUser.Schedules.Where(p => p.TimeblockID == id).FirstOrDefault();
            db.Schedules.Remove(mapping);
            db.SaveChanges();
            return RedirectToAction("Search");
        }

        public async Task AddFavoriteXX(int id)
        {
            User ourUser = GetUser();
            if (ourUser.UserID != id)
            {
                Follower newFollower = new Follower { UserID = ourUser.UserID, FollowingID = id };
                db.Followers.Add(newFollower);
                await db.SaveChangesAsync();
            }
        }

        public async Task AddFavorite(int id)
        {
            
                db.Followers.Add(new Follower { UserID = 1, FollowingID = 25 });
                await db.SaveChangesAsync();
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
