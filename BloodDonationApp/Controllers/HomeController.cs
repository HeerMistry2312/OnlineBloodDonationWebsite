using BloodDonationApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DatabaseLayer;

namespace BloodDonationApp.Controllers
{
    public class HomeController : Controller
    {
        ByteBridgeDbEntities DB = new ByteBridgeDbEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MainHome()
        {
            var message = ViewData["Message"] == null ? "Welcome To ByteBridege..." : ViewData["Message"];
            ViewData["Message"] = message;
            var registration = new RegistrationMV();
            ViewBag.UserTypeID = new SelectList(DB.UserTypeTables.Where(ut=>ut.UserTypeID > 1).ToList(), "UserTypeID", "UserType", "0");
            ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", "0");
            return View(registration);
        }
    }
}