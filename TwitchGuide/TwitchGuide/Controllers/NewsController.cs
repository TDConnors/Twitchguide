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
using PagedList;

namespace TwitchGuide.Controllers
{
    public class NewsController : BaseController
    {
        private TwitchContext db = new TwitchContext();

        public bool canChange()
        {
            if (isLoggedIn())
            {
                int temp = getUserID();
                if ((temp == (1)) || (temp == (3)) || (temp == (4)) || (temp == (27)))
                {
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }

        // GET: News
        public ActionResult Index(int? page)
        {
            ViewBag.canEdit = false;
            if (canChange())
            {
                ViewBag.canEdit = true;
            }
            //sorting
            var sorted = db.SiteNews.OrderByDescending((s => s.NewsID)).ToList();
            //paging
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(sorted.ToPagedList(pageNumber, pageSize));
        }

        // GET: News/Create
        public ActionResult Create()
        {
            if(canChange())
                return View();
            else
                return RedirectToAction("Index", "News");
        }

        // POST: News/Create
        // To protect from over-posting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NewsID,Title,Content,DateAdded")] News news)
        {

            if (ModelState.IsValid)
            {

                news.DateAdded = DateTime.Now;
                db.News.Add(news);
                db.SaveChanges();
                int current = getUserID();
                db.SiteNews.Add(new SiteNews { UserID = current, NewsID = news.NewsID });
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(news);
        }

        // GET: News/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SiteNews news = db.SiteNews.Where(p => p.NewsID == id).FirstOrDefault();

            if (news == null)
            {
                return HttpNotFound();
            }

            int currentId = getUserID();

            if ((currentId == news.UserID) || (currentId == 3))
                return View(news.News);
            else
                return RedirectToAction("Index", "News");
        }

        // POST: News/Edit/5
        // To protect from over-posting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NewsID,Title,Content,DateAdded")] News news)
        {
            if (ModelState.IsValid)
            {
                db.Entry(news).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(news);
        }

        // GET: News/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SiteNews news = db.SiteNews.Where(p => p.NewsID == id).FirstOrDefault();

            if (news == null)
            {
                return HttpNotFound();
            }

            int currentId = getUserID();

            if ((currentId == news.UserID) || (currentId == 3))
                return View(news.News);
            else
                return RedirectToAction("Index", "News");
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            News news = db.News.Find(id);
            db.News.Remove(news);
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
