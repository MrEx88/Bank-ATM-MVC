using AutomatedTellerMachine.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using AutomatedTellerMachine.Models;

namespace AutomatedTellerMachine.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET /home/index
        [Authorize]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var checkingAccountid = db.CheckingAccounts.Where(c => c.ApplicationUserId == userId).First().Id;
            ViewBag.CheckingAccountId = checkingAccountid;
            var manager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = manager.FindById(userId);
            ViewBag.Pin = user.Pin;
            return View();
        }

        //This attribute will display this in URL 
        //but if you don't have a view named "about-this-atm" you will get an error
        [ActionName("about-this-atm")]
        // GET /home/about
        [HandleError(View="Index")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        //GET /home/contact
        public ActionResult Contact()
        {
            ViewBag.TheMessage = "Having trouble? Send us a message.";

            return View();
        }

        //POST /home/contact
        [HttpPost]
        public ActionResult Contact(string message)
        {
            //TODO: end message to HQ
            ViewBag.TheMessage = "Thanks we got your message";

            return PartialView("_ContactThanks");
        }

        public ActionResult Foo()
        {
            return View("About");
        }

        public ActionResult Serial(string letterCase)
        {
            var serial = "ASPNETMVC5ATM1";
            if(letterCase == "lower")
            {
                return Content(serial.ToLower());
            }
            //return Content(serial);
            //return new HttpStatusCodeResult(403);
            //return Json(new {name = "serial", value = serial }, JsonRequestBehavior.AllowGet);
            return RedirectToAction("Index");
        }
    }
}