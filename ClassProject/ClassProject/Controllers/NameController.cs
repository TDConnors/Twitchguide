using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClassProject.Models;

namespace ClassProject.Controllers
{
    public class NameController : Controller
    {
        private classNameContext db = new classNameContext();

        // GET: Name
        public ActionResult Index()
        {
            return View(db.Names.ToList());
        }

        // GET: Name/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Name name = db.Names.Find(id);
            if (name == null)
            {
                return HttpNotFound();
            }
            return View(name);
        }

        // GET: Name/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Name/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NameID,Name1")] Name name)
        {
            if (ModelState.IsValid)
            {
                db.Names.Add(name);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(name);
        }

        // GET: Name/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Name name = db.Names.Find(id);
            if (name == null)
            {
                return HttpNotFound();
            }
            return View(name);
        }

        // POST: Name/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NameID,Name1")] Name name)
        {
            if (ModelState.IsValid)
            {
                db.Entry(name).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(name);
        }

        // GET: Name/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Name name = db.Names.Find(id);
            if (name == null)
            {
                return HttpNotFound();
            }
            return View(name);
        }

        // POST: Name/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Name name = db.Names.Find(id);
            db.Names.Remove(name);
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
