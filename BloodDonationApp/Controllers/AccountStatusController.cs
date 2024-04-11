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
    public class AccountStatusController : Controller
    {
        ByteBridgeDbEntities DB = new ByteBridgeDbEntities();

        public ActionResult AllAccountStatus()
        {
            var statuss = DB.AccountStatusTables.ToList();
            var liststatus = new List<AccountStatusMV>();
            foreach (var status in statuss)
            {
                var addStatus = new AccountStatusMV();
                addStatus.AccountStatusID = status.AccountStatusID;
                addStatus.AccountStatus = status.AccountStatus;
                liststatus.Add(addStatus);
            }
            return View(liststatus);
        }

        public ActionResult Create()
        {
            var status = new AccountStatusMV();
            return View(status);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AccountStatusMV AccountStatusMV)
        {
            if (ModelState.IsValid)
            {
                var checkStatus = DB.AccountStatusTables.Where(b => b.AccountStatus == AccountStatusMV.AccountStatus).FirstOrDefault();
                if (checkStatus == null)
                {
                    var accountStatusTable = new AccountStatusTable();
                    accountStatusTable.AccountStatusID = AccountStatusMV.AccountStatusID;
                    accountStatusTable.AccountStatus = AccountStatusMV.AccountStatus;
                    DB.AccountStatusTables.Add(accountStatusTable);
                    DB.SaveChanges();
                    return RedirectToAction("AllAccountStatus");
                }
                else
                {
                    ModelState.AddModelError("AccountStatus", "Already Exist!");
                }
            }
            return View(AccountStatusMV);
        }

        public ActionResult Edit(int? id)
        {
            var status = DB.AccountStatusTables.Find(id);
            if (status == null)
            {
                return HttpNotFound();

            }
            var AccountStatusMV = new AccountStatusMV();
            AccountStatusMV.AccountStatusID = status.AccountStatusID;
            AccountStatusMV.AccountStatus = status.AccountStatus;
            return View(AccountStatusMV);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AccountStatusMV AccountStatusMV)
        {
            if (ModelState.IsValid)
            {
                var checkStatus = DB.AccountStatusTables.Where(b => b.AccountStatus == AccountStatusMV.AccountStatus && b.AccountStatusID != AccountStatusMV.AccountStatusID).FirstOrDefault();
                if (checkStatus == null)
                {
                    var accountTable = new AccountStatusTable();
                    accountTable.AccountStatusID = AccountStatusMV.AccountStatusID;
                    accountTable.AccountStatus = AccountStatusMV.AccountStatus;
                    DB.Entry(accountTable).State = EntityState.Modified;
                    DB.SaveChanges();
                    return RedirectToAction("AllAccountStatus");
                }
                else
                {
                    ModelState.AddModelError("AccountStatus", "Already Exist!");
                }
            }
            return View(AccountStatusMV);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var status = DB.AccountStatusTables.Find(id);
            if (status == null)
            {
                return HttpNotFound();
            }
            var AccountStatusMV = new AccountStatusMV();
            AccountStatusMV.AccountStatusID = status.AccountStatusID;
            AccountStatusMV.AccountStatus = status.AccountStatus;
            return View(AccountStatusMV);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            var status = DB.AccountStatusTables.Find(id);
            DB.AccountStatusTables.Remove(status);
            DB.SaveChanges();
            return RedirectToAction("AllAccountStatus");
        }
    }
}