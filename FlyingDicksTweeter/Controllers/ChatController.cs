using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using FlyingDicksTweeter.Models;
using Chat = FlyingDicksTweeter.Models.Chat;

namespace FlyingDicksTweeter.Controllers
{
    public class ChatController : Controller
    {
        // GET: Chat
        public ActionResult StartChat(string name)
        {
            Session["user"] = name;
            return View("StartChat");
        }

        public ActionResult Chat(string msg)
        {

            Chat message = new Chat()
            {
                Name = this.User.Identity.Name,
                Date = DateTime.Now,
                Content = msg
            };
            var dbContext = new ApplicationDbContext();

            dbContext.Chat.Add(message);
            dbContext.SaveChanges();


            return PartialView("Message", message);
        }
    }
}