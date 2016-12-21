using System;
using System.Linq;
using System.Web.Mvc;
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
            using (var context = new ApplicationDbContext())
            {
                var messages = context.Chat.OrderByDescending(msg => msg.Date).ToList();
                ViewBag.messagesCount = messages.Count;
                ViewBag.messages = messages; 
            }
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

            using(var dbContext = new ApplicationDbContext())
            {
                dbContext.Chat.Add(message);
                dbContext.SaveChanges();
            }

            return PartialView("Message", message);
        }
    }
}