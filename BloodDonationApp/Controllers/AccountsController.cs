﻿using BloodDonationApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DatabaseLayer;

namespace BloodDonationApp.Controllers
{
    public class AccountsController : Controller
    {
        ByteBridgeDbEntities DB = new ByteBridgeDbEntities();
        public ActionResult AllNewUserRequest()
        {
           
            var users = DB.UserTables.Where(u => u.AccountStatusID == 1).ToList();

            return View(users);
        }
        public ActionResult UserDetails(int? id)
        {

            var user = DB.UserTables.Find(id);
            return View(user);
        }

        public ActionResult UserApproved(int? id)
        {
            var user = DB.UserTables.Find(id);
            user.AccountStatusID = 3;
            DB.Entry(user).State =System.Data.Entity.EntityState.Modified;
            DB.SaveChanges();
            return RedirectToAction("AllNewUserRequest");
        }
        public ActionResult UserRejected(int? id)
        {
            var user = DB.UserTables.Find(id);
            user.AccountStatusID = 4;
            DB.Entry(user).State = System.Data.Entity.EntityState.Modified;
            DB.SaveChanges();
            return RedirectToAction("AllNewUserRequest");
        }
    }
}