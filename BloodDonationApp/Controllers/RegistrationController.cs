using BloodDonationApp.Models;
using DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BloodDonationApp.Controllers
{
    public class RegistrationController : Controller
    {
        ByteBridgeDbEntities DB = new ByteBridgeDbEntities();
        static RegistrationMV registrationmv;
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SelectUser(RegistrationMV registrationMV)
        {
            registrationmv = registrationMV;
            if(registrationMV.UserTypeID == 2)
            {
                return RedirectToAction("DonorUser");
            }else if (registrationMV.UserTypeID == 3)
            {
                return RedirectToAction("SeekerUser");
            }
            else if (registrationMV.UserTypeID == 4)
            {
                return RedirectToAction("HospitalUser");
            }
            else if (registrationMV.UserTypeID == 5)
            {
                return RedirectToAction("BloodBankUser");
            }
            else
            {
                return RedirectToAction("MainHome", "Home");
            }
            
        }

        public ActionResult HospitalUser()
        {
            ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", registrationmv.CityID);
            return View(registrationmv);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HospitalUser(RegistrationMV registrationMV)
        {
            if (ModelState.IsValid)
            {
                var checktitle = DB.HospitalTables.Where(h => h.FullNAme == registrationMV.Hospital.FullNAme.Trim()).FirstOrDefault();
                if(checktitle == null)
                {
                    using (var transaction = DB.Database.BeginTransaction())
                    {
                        try
                        {
                            var user = new UserTable();
                            user.UserName = registrationMV.User.UserName;
                            user.Password = registrationMV.User.Password;
                            user.EmailAddress = registrationMV.User.EmailAddress;
                            user.AccountStatusID = 1;
                            user.UserTypeID = registrationMV.UserTypeID;
                            user.Description = registrationMV.User.Description;
                            DB.UserTables.Add(user);
                            DB.SaveChanges();

                            var hospital = new HospitalTable();
                            hospital.FullNAme = registrationMV.Hospital.FullNAme;
                            hospital.Address = registrationMV.Hospital.Address;
                            hospital.Location = registrationMV.Hospital.Address;
                            hospital.PhoneNo = registrationMV.Hospital.PhoneNo;
                            hospital.Website = registrationMV.Hospital.Website;
                            hospital.Email = registrationMV.Hospital.Email;
                            hospital.CityID = registrationMV.CityID;
                            hospital.UserID = user.UserID;
                            DB.HospitalTables.Add(hospital);

                            DB.SaveChanges();
                            transaction.Commit();
                            ViewData["Message"] = "Thanks For registration, Your Query will be Review Shortly!!!";
                            return RedirectToAction("MainHome", "Home");
                        }
                        catch 
                        {
                            ModelState.AddModelError(string.Empty, "Please Provide Correct Information...");
                            transaction.Rollback();
                        }
                    }

                }else
                {
                    ModelState.AddModelError(string.Empty, "Hospital Already Registered...");
                }
            }
            ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", registrationMV.CityID);
            return View(registrationMV);
        }

        public ActionResult DonorUser()
        {
            ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", registrationmv.CityID);
            ViewBag.BloodGroupID = new SelectList(DB.BloodGroupsTables.ToList(),"BloodGroupID","BloodGroup","0");
            ViewBag.GenderID = new SelectList(DB.GenderTables.ToList(), "GenderID", "Gender", "0");
            return View(registrationmv);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DonorUser(RegistrationMV registrationMV)
        {
            if (ModelState.IsValid)
            {
                var checktitle = DB.DonorTables.Where(h => h.FullName == registrationMV.Donor.FullName.Trim() && h.CNIC == registrationMV.Donor.CNIC).FirstOrDefault();
                if (checktitle == null)
                {
                    using (var transaction = DB.Database.BeginTransaction())
                    {
                        try
                        {
                            var user = new UserTable();
                            user.UserName = registrationMV.User.UserName;
                            user.Password = registrationMV.User.Password;
                            user.EmailAddress = registrationMV.User.EmailAddress;
                            user.AccountStatusID = 1;
                            user.UserTypeID = registrationMV.UserTypeID;
                            user.Description = registrationMV.User.Description;
                            DB.UserTables.Add(user);
                            DB.SaveChanges();


                            var donor = new DonorTable();
                            donor.FullName = registrationMV.Donor.FullName;
                            donor.BloodGroupID = registrationMV.BloodGroupID;
                            donor.ContactNo = registrationMV.Donor.ContactNo;
                            donor.CNIC = registrationMV.Donor.CNIC;
                            donor.GenderID = registrationMV.GenderID;
                            donor.Location = registrationMV.Donor.Location;
                            donor.LastDonationDate = registrationMV.Donor.LastDonationDate;
                            donor.CityID = registrationMV.CityID;
                            donor.UserID = user.UserID;
                            DB.DonorTables.Add(donor);

                            DB.SaveChanges();
                            transaction.Commit();
                            ViewData["Message"] = "Thanks For registration, Your Query will be Review Shortly!!!";
                            return RedirectToAction("MainHome", "Home");
                        }
                        catch
                        {
                            ModelState.AddModelError(string.Empty, "Please Provide Correct Information...");
                            transaction.Rollback();
                        }
                    }

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Donor Already Registered...");
                }
            }
            ViewBag.BloodGroupID = new SelectList(DB.BloodGroupsTables.ToList(), "BloodGroupID", "BloodGroup", registrationmv.BloodGroupID);
            ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", registrationMV.CityID);
            ViewBag.GenderID = new SelectList(DB.GenderTables.ToList(), "GenderID", "Gender", registrationMV.GenderID);
            return View(registrationMV);
        }

        public ActionResult BloodBankUser()
        {
            ViewBag.UserTypeID = new SelectList(DB.UserTypeTables.Where(ut => ut.UserTypeID > 1).ToList(), "UserTypeID", "UserType", registrationmv.UserTypeID);
            ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", registrationmv.CityID);
            return View(registrationmv);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BloodBankUser(RegistrationMV registrationMV)
        {
            if (ModelState.IsValid)
            {
                var checktitle = DB.BloodBankTables.Where(h => h.BloodBankName == registrationMV.BloodBank.BloodBankName.Trim() && h.PhoneNo == registrationMV.BloodBank.PhoneNo).FirstOrDefault();
                if (checktitle == null)
                {
                    using (var transaction = DB.Database.BeginTransaction())
                    {
                        try
                        {
                            var user = new UserTable();
                            user.UserName = registrationMV.User.UserName;
                            user.Password = registrationMV.User.Password;
                            user.EmailAddress = registrationMV.User.EmailAddress;
                            user.AccountStatusID = 1;
                            user.UserTypeID = registrationMV.UserTypeID;
                            user.Description = registrationMV.User.Description;
                            DB.UserTables.Add(user);
                            DB.SaveChanges();


                            var bloodBank = new BloodBankTable();
                            bloodBank.BloodBankName = registrationMV.BloodBank.BloodBankName;
                            bloodBank.PhoneNo = registrationMV.BloodBank.PhoneNo;
                            bloodBank.Address = registrationMV.BloodBank.Address;
                            bloodBank.Location = registrationMV.BloodBank.Address;
                            bloodBank.Website = registrationMV.BloodBank.Website;
                            bloodBank.Email = registrationMV.BloodBank.Email;
                            bloodBank.CityID = registrationMV.CityID;
                            bloodBank.UserID = user.UserID;
                            DB.BloodBankTables.Add(bloodBank);

                            DB.SaveChanges();
                            transaction.Commit();
                            ViewData["Message"] = "Thanks For registration, Your Query will be Review Shortly!!!";
                            return RedirectToAction("MainHome", "Home");
                        }
                        catch
                        {
                            ModelState.AddModelError(string.Empty, "Please Provide Correct Information...");
                            transaction.Rollback();
                        }
                    }

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Blood Bank Already Registered...");
                }
            }
            
            ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", registrationMV.CityID);
           
            return View(registrationMV);
        }

        public ActionResult SeekerUser()
        {
           ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", registrationmv.CityID);
            return View(registrationmv);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SeekerUser(RegistrationMV registrationMV)
        {
            ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", registrationmv.CityID);
            return View();
        }
    }
}