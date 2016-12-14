using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FlyingDicksTweeter.Models;

namespace FlyingDicksTweeter.Controllers
{
    public class SearchController : Controller
    {
        private SearchModel db = new SearchModel();

        public ActionResult SearchName(string searchName)
        {
            var users = from s in db.AspNetUsers select s;

            if (!String.IsNullOrEmpty(searchName))
            {
                users = users.Where(c => c.FullName.Contains(searchName));
            }
            return View(users);
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
