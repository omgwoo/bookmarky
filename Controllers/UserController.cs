using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Bookmarky.Entities;
using Bookmarky.Models;
using DevOne.Security.Cryptography.BCrypt;
using System.Net.Mail;


namespace Bookmarky
{
    public class UserController : Controller
    {
        private redxBookmarkyEntities db = new redxBookmarkyEntities();

        //// GET: Users
        //public ActionResult Index()
        //{
        //    return View(db.Users.ToList());
        //}

        //// GET: Users/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    User user = db.Users.Find(id);
        //    if (user == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(user);
        //}


        [HttpGet]
        [AllowAnonymous]
        public string LoginFromPlugin(string Username, string Password)
        {
            if (BCryptHelper.CheckPassword(Password + "*)&h9", db.Users.First(u => u.Username == Username).Password))
            {
                
                return "true";
            }
            return "false";
        }

        [Authorize]
        public ActionResult Logout()
        {
            if (User.Identity.Name != null)
            {
                FormsAuthentication.SignOut();
            }
            return RedirectToAction("Index", "Home");

        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login([Bind(Include = "Username, Password")] UserLoginViewModel userLVM)
        {
            if (ModelState.IsValid)
            {
                if (BCryptHelper.CheckPassword(userLVM.Password + "*)&h9", db.Users.First(u => u.Username == userLVM.Username).Password))
                {
                    //BCrypt.CheckPassword(userEnteredPassword + "^Y8~JJ", hashedPwdFromDatabase);
                    //User.Identity.IsAuthenticated = true;
                    //User.Identity.Name = userLVM.Username;
                    FormsAuthentication.SetAuthCookie(userLVM.Username, false);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(userLVM);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "UserID,Username,Password,Salt,Email,IsEmailVerified,IsActive")] User user)
        public ActionResult Create([Bind(Include = "Username,Password,ConfirmPassword, Email")] UserCreateViewModel userVM)
        {
            if (db.Users.Any(u => u.Email == userVM.Email))
            {
                ModelState.AddModelError("Email", "Email in use");
            }

            if (db.Users.Any(u => u.Username == userVM.Username))
            {
                ModelState.AddModelError("Username", "Username in use");
            }

            //UserID,Salt,IsEmailVerified,IsActive;
            if (ModelState.IsValid)
            {

                User user = new User();
                user.Username = userVM.Username;
                //user.Password = userVM.Password;
                user.Email = userVM.Email;
                //user.Salt = BCryptHelper.GenerateSalt();
                string pwdToHash = userVM.Password + "*)&h9";
                user.Password = BCryptHelper.HashPassword(pwdToHash, BCryptHelper.GenerateSalt());

                //db.Users.Add(user);
                //db.SaveChanges();
                //try
                //{
                //    db.SaveChanges();
                //}
                //catch (DbUpdateException e)
                //{
                //    //if(e.InnerException.ToString().Contains("Cannot insert duplicate key row in object 'redxadmin.Users' with unique index 'IX_Email'"))
                //    if(db.Users.Any(u => u.Email == userVM.Email))
                //    {
                //        ModelState.AddModelError("Email", "Email in use");
                //    }
                //    //if (e.InnerException.ToString().Contains("Cannot insert duplicate key row in object 'redxadmin.Users' with unique index 'IX_Username'"))
                //    if(db.Users.Any(u => u.Username == userVM.Username))
                //    {
                //        ModelState.AddModelError("Username", "Username in use");
                //    }
                //    return View(userVM);
                //}
                FormsAuthentication.SetAuthCookie(userVM.Username, false);
                
                //MailMessage mMsg = new MailMessage("bookmarky@redx.rocks", user.Email);
                //mMsg.Subject = "Account created";
                //mMsg.Body = "An account with username " + user.Username + " has been created on bookmarky.redx.rocks";
                //SmtpClient smtpClient = new SmtpClient();
                //smtpClient.Send(mMsg);
                //smtpClient.Dispose();

                db.Users.Add(user);
                db.SaveChanges();

                UserAuthToken uat = new UserAuthToken();
                uat.UserID = user.UserID;
                uat.AuthToken = BCryptHelper.GenerateSalt();
                uat.ExpireDate = DateTime.Now;

                db.UserAuthTokens.Add(uat);
                db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            return View(userVM);
        }
    }

    //    // GET: Users/Edit/5
    //    public ActionResult Edit(int? id)
    //    {
    //        if (id == null)
    //        {
    //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //        }
    //        User user = db.Users.Find(id);
    //        if (user == null)
    //        {
    //            return HttpNotFound();
    //        }
    //        return View(user);
    //    }

    //    // POST: Users/Edit/5
    //    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    //    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public ActionResult Edit([Bind(Include = "UserID,Username,Password,Salt,Email,IsEmailVerified,IsActive")] User user)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            db.Entry(user).State = EntityState.Modified;
    //            db.SaveChanges();
    //            return RedirectToAction("Index");
    //        }
    //        return View(user);
    //    }

    //    // GET: Users/Delete/5
    //    public ActionResult Delete(int? id)
    //    {
    //        if (id == null)
    //        {
    //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //        }
    //        User user = db.Users.Find(id);
    //        if (user == null)
    //        {
    //            return HttpNotFound();
    //        }
    //        return View(user);
    //    }

    //    // POST: Users/Delete/5
    //    [HttpPost, ActionName("Delete")]
    //    [ValidateAntiForgeryToken]
    //    public ActionResult DeleteConfirmed(int id)
    //    {
    //        User user = db.Users.Find(id);
    //        db.Users.Remove(user);
    //        db.SaveChanges();
    //        return RedirectToAction("Index");
    //    }

    //    protected override void Dispose(bool disposing)
    //    {
    //        if (disposing)
    //        {
    //            db.Dispose();
    //        }
    //        base.Dispose(disposing);
    //    }
    //}
}
