﻿using DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;
using BloodDonationApp.Models;

namespace BloodDonationApp.Controllers
{
    public class RequestTypeController : Controller
    {
        ByteBridgeDbEntities DB = new ByteBridgeDbEntities();

        public ActionResult AllRequestTypes()
        {
            var requesttypes = DB.RequestTypeTAbles.ToList();
            var listrequesttypes = new List<RequestTypeMV>();
            foreach (var requesttype in requesttypes)
            {
                var addrequesttype = new RequestTypeMV();
                addrequesttype.RequestTypeID = requesttype.RequestTypeID;
                addrequesttype.RequestType = requesttype.RequestType;
                listrequesttypes.Add(addrequesttype);
            }
            return View(listrequesttypes);
        }
        public ActionResult Create()
        {
            var requesttype = new RequestTypeMV();
            return View(requesttype);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RequestTypeMV requestTypeMV)
        {
            if (ModelState.IsValid)
            {
                var requestTypeTAble = new RequestTypeTAble();
                requestTypeTAble.RequestTypeID = requestTypeMV.RequestTypeID;
                requestTypeTAble.RequestType = requestTypeMV.RequestType;
                DB.RequestTypeTAbles.Add(requestTypeTAble);
                DB.SaveChanges();
                return RedirectToAction("AllRequestTypes");
            }
            return View(requestTypeMV);
        }
        public ActionResult Edit(int? id)
        {
            var requesttype = DB.RequestTypeTAbles.Find(id);
            if(requesttype == null)
            {
                return HttpNotFound();

            }
            var requesttypemv = new RequestTypeMV();
            requesttypemv.RequestTypeID = requesttype.RequestTypeID;
            requesttypemv.RequestType = requesttypemv.RequestType;
            return View(requesttypemv);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RequestTypeMV requestTypeMV)
        {
            if(ModelState.IsValid)
            {
                var requestTypeTAble = new RequestTypeTAble();
                requestTypeTAble.RequestTypeID = requestTypeMV.RequestTypeID;
                requestTypeTAble.RequestType = requestTypeMV.RequestType;
                DB.Entry(requestTypeTAble).State = EntityState.Modified;
                DB.SaveChanges();
                return RedirectToAction("AllRequestTypes");
            }
            return View(requestTypeMV);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var requesttype = DB.RequestTypeTAbles.Find(id);
            if(requesttype == null)
            {
                return HttpNotFound();
            }
            var requesttypemv = new RequestTypeMV();
            requesttypemv.RequestTypeID = requesttype.RequestTypeID;
            requesttypemv.RequestType = requesttypemv.RequestType;
            return View(requesttypemv);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            var requesttype = DB.RequestTypeTAbles.Find(id);
            DB.RequestTypeTAbles.Remove(requesttype);
            DB.SaveChanges();
            return RedirectToAction("AllRequestTypes");
        }
    }
}