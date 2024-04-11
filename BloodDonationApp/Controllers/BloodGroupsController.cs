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
    public class BloodGroupsController : Controller
    {
        ByteBridgeDbEntities DB = new ByteBridgeDbEntities();

        public ActionResult AllBloodGroups()
        {
            var bloodGroups = DB.BloodGroupsTables.ToList();
            var listBloodGroups = new List<BloodGroupsMV>();
            foreach (var bloodGroup in bloodGroups)
            {
                var addBloodGroup = new BloodGroupsMV();
                addBloodGroup.BloodGroupID = bloodGroup.BloodGroupID;
                addBloodGroup.BloodGroup = bloodGroup.BloodGroup;
                listBloodGroups.Add(addBloodGroup);
            }
            return View(listBloodGroups);
        }

        public ActionResult Create()
        {
            var bloodGroup = new BloodGroupsMV();
            return View(bloodGroup);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BloodGroupsMV bloodGroupsMV)
        {
            if (ModelState.IsValid)
            {
                var checkBloodGroup = DB.BloodGroupsTables.Where(b => b.BloodGroup == bloodGroupsMV.BloodGroup).FirstOrDefault();
                if (checkBloodGroup == null)
                {
                    var bloodGroupsTable = new BloodGroupsTable();
                    bloodGroupsTable.BloodGroupID = bloodGroupsMV.BloodGroupID;
                    bloodGroupsTable.BloodGroup = bloodGroupsMV.BloodGroup;
                    DB.BloodGroupsTables.Add(bloodGroupsTable);
                    DB.SaveChanges();
                    return RedirectToAction("AllBloodGroups");
                }
                else
                {
                    ModelState.AddModelError("BloodGroup", "Already Exist!");
                }
            }
            return View(bloodGroupsMV);
        }

        public ActionResult Edit(int? id)
        {
            var bloodGroup = DB.BloodGroupsTables.Find(id);
            if (bloodGroup == null)
            {
                return HttpNotFound();

            }
            var bloodGroupsMV = new BloodGroupsMV();
            bloodGroupsMV.BloodGroupID = bloodGroup.BloodGroupID;
            bloodGroupsMV.BloodGroup = bloodGroup.BloodGroup;
            return View(bloodGroupsMV);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BloodGroupsMV bloodGroupsMV)
        {
            if (ModelState.IsValid)
            {
            var checkBloodGroup = DB.BloodGroupsTables.Where(b => b.BloodGroup == bloodGroupsMV.BloodGroup && b.BloodGroupID != bloodGroupsMV.BloodGroupID).FirstOrDefault();
            if (checkBloodGroup == null)
            {
                var bloodGroupsTable = new BloodGroupsTable();
                bloodGroupsTable.BloodGroupID = bloodGroupsMV.BloodGroupID;
                bloodGroupsTable.BloodGroup = bloodGroupsMV.BloodGroup;
                DB.Entry(bloodGroupsTable).State = EntityState.Modified;
                DB.SaveChanges();
                return RedirectToAction("AllBloodGroups");
                }
                else
                {
                    ModelState.AddModelError("BloodGroup", "Already Exist!");
                }
            }
            return View(bloodGroupsMV);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var bloodGroups = DB.BloodGroupsTables.Find(id);
            if (bloodGroups == null)
            {
                return HttpNotFound();
            }
            var bloodGroupsMV = new BloodGroupsMV();
            bloodGroupsMV.BloodGroupID = bloodGroups.BloodGroupID;
            bloodGroupsMV.BloodGroup = bloodGroups.BloodGroup;
            return View(bloodGroupsMV);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            var bloodGroups = DB.BloodGroupsTables.Find(id);
            DB.BloodGroupsTables.Remove(bloodGroups);
            DB.SaveChanges();
            return RedirectToAction("AllBloodGroups");
        }
    }
}