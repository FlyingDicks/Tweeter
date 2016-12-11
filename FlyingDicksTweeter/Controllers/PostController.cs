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
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
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

        public ActionResult Delete(int? id)
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

                if (! IsUserAuthorizedToEdit(post))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                }

                if (post == null)
                {
                    return HttpNotFound();
                }

                return View(post);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
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

                if (post == null)
                {
                    return HttpNotFound();
                }

                database.Posts.Remove(post);
                database.SaveChanges();

                return RedirectToAction("Index");
            }
        }


        public ActionResult Edit(int? id)
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

                if (!IsUserAuthorizedToEdit(post))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                }

                if (post == null)
                {
                    return HttpNotFound();
                }

                var model = new PostViewModel();
                model.Id = post.Id;
                model.Content = post.Content;

                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Edit(PostViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var database = new ApplicationDbContext())
                {
                    var post = database.Posts
                        .FirstOrDefault(a => a.Id == model.Id);

                    post.Content = model.Content;

                    database.Entry(post).State = EntityState.Modified;
                    database.SaveChanges();

                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }

        private bool IsUserAuthorizedToEdit(Post post)
        {
            bool isAdmin = this.User.IsInRole("Admin");
            bool isAuthor = post.IsAuthor(this.User.Identity.Name);

            return isAdmin || isAuthor;
        }
    }
}