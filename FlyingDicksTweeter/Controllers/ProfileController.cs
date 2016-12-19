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
                .ToList();

            var username = user.FullName;

            ViewBag.Username = username;

            return View(picsByUser);
        }
        public ActionResult Slider(int id)
        {
            var db = new ApplicationDbContext();


            var post = db.Posts.Find(id);

            var IQueryable = db.Posts.Where(a => a.Id == post.Id).Select(a => a.Author.Id);

            var userID = (from Comp in db.Posts where (Comp.Id == post.Id) select Comp.Author.Id).Single();

            var user = db.Users.Find(userID);

            var picsByUser = db.Posts.Where(a => a.Author.Id == user.Id)
                .OrderByDescending(a => a.DateAdded)
                .Select(a => a.Image)
                .ToList();

            for (int i = 0; i < picsByUser.Count; i++)
            {
                if (picsByUser == null)
                {
                    picsByUser.RemoveAt(i);
                }
            }
            var picFromWhereToStart = post.Image;


            for (int i = 0; i < picsByUser.Count; i++)
            {
                if (Convert.ToBase64String(picsByUser[0]) == Convert.ToBase64String(picFromWhereToStart))
                {
                    break;
                }
                var a = picsByUser[0];
                picsByUser.RemoveAt(0);
                picsByUser.Add(a);
            }

            return View(picsByUser);
        }
    }
}