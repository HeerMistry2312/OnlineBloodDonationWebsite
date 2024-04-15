using BloodDonationApp.Models;
using DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BloodDonationApp.Controllers
{
    public class UserTypeController : Controller
    {
        ByteBridgeDbEntities DB = new ByteBridgeDbEntities();

        public ActionResult AllUserTypes()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var userTypes = DB.UserTypeTables.ToList();
            var listUserTypes = new List<UserTypeMV>();
            foreach (var userType in userTypes)
            {
                var adduserType = new UserTypeMV();
                adduserType.UserTypeID = userType.UserTypeID;
                adduserType.UserType = userType.UserType;
                listUserTypes.Add(adduserType);
            }
            return View(listUserTypes);
        }

        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var userType = new UserTypeMV();
            return View(userType);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserTypeMV userTypeMV)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                var checkUserType = DB.UserTypeTables.Where(b => b.UserType == userTypeMV.UserType).FirstOrDefault();
                if (checkUserType == null)
                {
                    var userTypeTable = new UserTypeTable();
                userTypeTable.UserTypeID = userTypeMV.UserTypeID;
                userTypeTable.UserType = userTypeMV.UserType;
                DB.UserTypeTables.Add(userTypeTable);
                DB.SaveChanges();
                return RedirectToAction("AllUserTypes");
                }
                else
                {
                    ModelState.AddModelError("UserType", "Already Exist!");
                }
            }
            return View(userTypeMV);
        }

        public ActionResult Edit(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var userType = DB.UserTypeTables.Find(id);
            if (userType == null)
            {
                return HttpNotFound();

            }
            var userTypemv = new UserTypeMV();
            userTypemv.UserTypeID = userType.UserTypeID;
            userTypemv.UserType = userType.UserType;
            return View(userTypemv);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserTypeMV userTypeMV)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                var checkUserType = DB.UserTypeTables.Where(b => b.UserType == userTypeMV.UserType && b.UserTypeID != userTypeMV.UserTypeID).FirstOrDefault();
                if (checkUserType == null)
                {
                    var userTypeTable = new UserTypeTable();
                userTypeTable.UserTypeID = userTypeMV.UserTypeID;
                userTypeTable.UserType = userTypeMV.UserType;
                DB.Entry(userTypeTable).State = EntityState.Modified;
                DB.SaveChanges();
                return RedirectToAction("AllUserTypes");
                }
                else
                {
                    ModelState.AddModelError("UserType", "Already Exist!");
                }
            }
            return View(userTypeMV);
        }

        public ActionResult Delete(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userType = DB.UserTypeTables.Find(id);
            if (userType == null)
            {
                return HttpNotFound();
            }
            var userTypeMV = new UserTypeMV();
            userTypeMV.UserTypeID = userType.UserTypeID;
            userTypeMV.UserType = userType.UserType;
            return View(userTypeMV);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var userType = DB.UserTypeTables.Find(id);
            DB.UserTypeTables.Remove(userType);
            DB.SaveChanges();
            return RedirectToAction("AllUserTypes");
        }
    }
}