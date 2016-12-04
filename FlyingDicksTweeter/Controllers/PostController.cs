using FlyingDicksTweeter.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FlyingDicksTweeter.Controllers
{
    public class PostController : Controller
    {
        // GET: Post
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            using (var database = new ApplicationDbContext())
            {
                var posts = database.Posts
                    .Include(a => a.Author)
                    .ToList();
                
                return View(posts);
            }
        }

        public ActionResult Details (int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var database = new ApplicationDbContext())
            {
                var post = database.Posts
                    .Where(a => a.Id == id)
                    .Include(a => a.Author)
                    .First();

                return View(post);
            }
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Post post)
        {
            if (ModelState.IsValid)
            {
                using (var database = new ApplicationDbContext())
                {
                    var author = database.Users
                        .Where(u => u.UserName == this.User.Identity.Name)
                        .First();

                    post.Author = author;

                    database.Posts.Add(post);
                    database.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            return View(post);
        }
    }
}