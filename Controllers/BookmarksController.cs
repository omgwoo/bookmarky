using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bookmarky.Entities;

namespace Bookmarky.Controllers
{
    public class BookmarksController : Controller
    {
        private redxBookmarkyEntities db = new redxBookmarkyEntities();


        [HttpPost]
        [AllowAnonymous]
        public void PostFromPlugin(string url, string authtoken, string comment)
        {
            if (url.Length > 0)
            {
                Bookmark bookmark = new Bookmark();
                bookmark.URL = url;

                UserAuthToken uat = new UserAuthToken();
                uat = db.UserAuthTokens.FirstOrDefault(u => u.AuthToken == authtoken);
                bookmark.UserID = uat.UserID;
                bookmark.Comment = comment;

                db.Bookmarks.Add(bookmark);
                db.SaveChanges();
            
            }            
        }


        // GET: Bookmarks
        [Authorize]
        public ActionResult Index()
        {
            User userID = new User();
            userID = db.Users.FirstOrDefault(u => u.Username == User.Identity.Name);
            var myBookmarks = db.Bookmarks.Where(b => b.UserID == userID.UserID);
            return View(myBookmarks.ToList());
        }

        // GET: Bookmarks/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Bookmark bookmark = db.Bookmarks.Find(id);
        //    if (bookmark == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(bookmark);
        //}

        //// GET: Bookmarks/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Bookmarks/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "BookmarkID,UserID,URL,Comment")] Bookmark bookmark)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Bookmarks.Add(bookmark);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(bookmark);
        //}

        //// GET: Bookmarks/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Bookmark bookmark = db.Bookmarks.Find(id);
        //    if (bookmark == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(bookmark);
        //}

        //// POST: Bookmarks/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "BookmarkID,UserID,URL,Comment")] Bookmark bookmark)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(bookmark).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(bookmark);
        //}

        //// GET: Bookmarks/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Bookmark bookmark = db.Bookmarks.Find(id);
        //    if (bookmark == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(bookmark);
        //}

        //// POST: Bookmarks/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Bookmark bookmark = db.Bookmarks.Find(id);
        //    db.Bookmarks.Remove(bookmark);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
