using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bookmarky.Models;
using Bookmarky.Entities;

namespace Bookmarky.Controllers
{
    public class HomeController : Controller
    {
        private redxBookmarkyEntities db = new redxBookmarkyEntities();

        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                User userID = new User();
                userID = db.Users.FirstOrDefault(u => u.Username == User.Identity.Name);
                UserAuthToken uat = new UserAuthToken();
                uat = db.UserAuthTokens.First(u => u.UserID == userID.UserID);
                ViewBag.uat = uat.AuthToken;
            }
                return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}