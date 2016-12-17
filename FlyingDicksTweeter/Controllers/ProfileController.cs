using FlyingDicksTweeter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlyingDicksTweeter.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Profile(string id)
        {
            var db = new ApplicationDbContext();

            var user = db.Users.Find(id);

            var postsByUser = db.Posts.Where(a => a.Author.Id == user.Id).OrderByDescending(a=>a.DateAdded).ToList();

            var username = user.FullName;

            ViewBag.Username = username;

            return View(postsByUser);
        }
        public ActionResult Gallery(string id)
        {
            var context = new ApplicationDbContext();

            var user = context.Users.Find(id);

            var picsByUser = context.Posts.Where(a => a.Author.Id == user.Id)
                .OrderByDescending(a => a.DateAdded)
                .Select(a => a.Image)
                .ToList();

            var username = user.FullName;

            ViewBag.Username = username;

            return View(picsByUser);
        }
        public ActionResult Slider()
        {
            //var db = new ApplicationDbContext();

            //var pic = db.Posts.Find(picID);

            //var userID = pic.Author.Id;

            //var user = db.Users.Find(userID);

            //var picsByUser = db.Posts.Where(a => a.Author.Id == user.Id)
            //    .OrderByDescending(a => a.DateAdded)
            //    .Select(a => a.Image)
            //    .ToList();

            //for (int i = 0; i < picsByUser.Count; i++)
            //{
            //    if(picsByUser[0] == pic.Image)
            //    {
            //        break;
            //    }
            //    var a = picsByUser[0];
            //    picsByUser.RemoveAt(0);
            //    picsByUser.Add(a);
            //}

            return View();
        }
    }
}