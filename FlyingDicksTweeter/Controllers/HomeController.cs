using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlyingDicksTweeter.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace FlyingDicksTweeter.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("List", "Post");
            }

            return View("Index");
        }

        public FileContentResult UserPhotos()
        {
            FileContentResult result;

            if (User.Identity.IsAuthenticated)
            {
                string userId = User.Identity.GetUserId();

                if (userId == null)
                {
                    string fileName = HttpContext.Server.MapPath(@"~/Images/noImg.svg");

                    byte[] imageData = null;
                    FileInfo fileInfo = new FileInfo(fileName);
                    long imageFileLength = fileInfo.Length;
                    using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                    {

                        using (BinaryReader br = new BinaryReader(fs))
                        {
                            imageData = br.ReadBytes((int)imageFileLength);
                            return File(imageData, "image/png");
                        }
                    }
                }

                // to get the user details to load user Image 
                using (var dbUsers = HttpContext.GetOwinContext().Get<ApplicationDbContext>())
                {
                    var user = dbUsers.Users.Where(x => x.Id == userId).FirstOrDefault();
                    if (user != null && user.UserPhoto != null)
                    {
                        result = new FileContentResult(user?.UserPhoto, "image/jpeg");
                    }
                    else
                    {
                        result = GetDefaultImage();
                    }
                }
            }
            else
            {
                result = GetDefaultImage();
            }

            return result;
        }

        private FileContentResult GetDefaultImage()
        {
            string fileName = HttpContext.Server.MapPath(@"~/Images/noImg.svg");

            byte[] imageData = null;
            FileInfo fileInfo = new FileInfo(fileName);
            long imageFileLength = fileInfo.Length;
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            imageData = br.ReadBytes((int)imageFileLength);
            return File(imageData, "image/png");
        }
    }
}