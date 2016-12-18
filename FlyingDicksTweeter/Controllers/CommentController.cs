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
    [Authorize]
    public class CommentController : Controller
    {
        [HttpPost]
        public ActionResult Edit(CommentViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new ApplicationDbContext())
                {
                    Comment comment = db.Comments.Where(c => c.Id == model.Id).First();
                    comment.Content = model.Content;

                    db.Entry(comment).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Details", "Post", new { id = model.PostId });
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Delete(CommentViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new ApplicationDbContext())
                {
                    Comment comment = db.Comments.Where(c => c.Id == model.Id).First();

                    db.Comments.Remove(comment);
                    db.SaveChanges();
                    return RedirectToAction("Details", "Post", new { id = model.PostId });
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Create(Comment model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new ApplicationDbContext())
                {
                    Comment comment = new Comment();
                    comment.PostId = model.PostId;
                    comment.Content = model.Content;
                    var author = db.Users.Where(u => u.UserName == this.User.Identity.Name).First();

                    comment.Author = author;

                    db.Comments.Add(comment);

                    db.SaveChanges();

                }
                return RedirectToAction("Details", "Post", new { id = model.PostId });
            }

            return RedirectToAction("Index", "Home");
        }
    }
}