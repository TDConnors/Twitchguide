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
    public class TimeblocksController : Controller
    {
        private TwitchContext db = new TwitchContext();

        // GET: Timeblocks
        public ActionResult Index()
        {
            return View(db.Timeblocks.ToList());
        }


        // GET: Timeblocks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Timeblocks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StartHour,StartMinute,EndHour,EndMinute,Day")] Timeblock timeblock)
        {
            if (ModelState.IsValid)
            {
                db.Timeblocks.Add(timeblock);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(timeblock);
        }


        // GET: Timeblocks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Timeblock timeblock = db.Timeblocks.Find(id);
            if (timeblock == null)
            {
                return HttpNotFound();
            }
            return View(timeblock);
        }

        // POST: Timeblocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Timeblock timeblock = db.Timeblocks.Find(id);
            db.Timeblocks.Remove(timeblock);
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
