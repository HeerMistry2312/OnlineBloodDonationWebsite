using BloodDonationApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DatabaseLayer;
using System.ComponentModel.DataAnnotations;

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
        public ActionResult Login()
        {
            var userMV = new UserMV();
            return View(userMV);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserMV userMV)
        {
            if (ModelState.IsValid)
            {
                var user = DB.UserTables.Where(u => u.Password == userMV.Password && u.UserName == userMV.UserName).FirstOrDefault();
                if (user != null)
                {
                    if(user.AccountStatusID == 1)
                    {
                        ModelState.AddModelError(string.Empty, "Please Wait! your Account is Under-Review.");
                    }else if (user.AccountStatusID == 4)
                    {
                        ModelState.AddModelError(string.Empty, "Your Account Is Rejected!");
                    }
                    else if (user.AccountStatusID == 5)
                    {
                        ModelState.AddModelError(string.Empty, "Your Account Is Suspended!");
                    }
                    else if (user.AccountStatusID == 3)
                    {
                        Session["UserID"] = user.UserID;
                        Session["UserName"] = user.UserName;
                        Session["Password"] = user.Password;
                        Session["EmailAddress"] = user.EmailAddress;
                        Session["AccountStatusID"] = user.AccountStatusID;
                        Session["AccountStatus"] = user.AccountStatusTable.AccountStatus;
                        Session["UserTypeID"] = user.UserTypeID;
                        Session["UserType"] = user.UserTypeTable.UserType;
                        Session["Description"] = user.Description;

                        
                        if(user.UserTypeID == 1) //Admin Session
                        {
                            return RedirectToAction("MainHome");
                        }
                        else if(user.UserTypeID == 2) //Donor Session

                        {
                            var donor = DB.DonorTables.Where(u => u.UserID == user.UserID).FirstOrDefault();
                            if (donor != null) 
                            {
                                Session["DonorId"] = donor.DonorId;
                                Session["FullName"] = donor.FullName;
                                Session["GenderID"] = donor.GenderID;
                                Session["BloodGroupID"] = donor.BloodGroupID;
                                Session["BloodGroup"] = donor.BloodGroupsTable.BloodGroup;
                                Session["LastDonationDate"] = donor.LastDonationDate;
                                Session["Location"] = donor.Location;
                                Session["ContactNo"] = donor.ContactNo;
                                Session["CNIC"] = donor.CNIC;
                                Session["CityID"] = donor.CityID;
                                Session["City"] = donor.CityTable.City;
                                
                            }
                            else
                            {
                               
                                ModelState.AddModelError(string.Empty, "UserName or Password is Incorrect.");
                            }
                        }
                        else if (user.UserTypeID == 3) //Seeker Session

                        {
                            var seeker = DB.SeekerTables.Where(u => u.UserID == user.UserID).FirstOrDefault();
                            if (seeker != null)
                            {
                                Session["SeekerID"] = seeker.SeekerID;
                                Session["FullName"] = seeker.FullName;
                                Session["Age"] = seeker.Age;
                                Session["CityID"] = seeker.CityID;
                                Session["City"] = seeker.CityTable.City;
                                Session["BloodGroupID"] = seeker.BloodGroupID;
                                Session["BloodGroup"] = seeker.BloodGroupsTable.BloodGroup;
                                Session["ContactNo"] = seeker.ContactNo;
                                Session["CNIC"] = seeker.CNIC;
                                Session["GenderID"] = seeker.GenderID;
                                Session["Gender"] = seeker.GenderTable.Gender;
                                Session["RegistrationDate"] = seeker.RegistrationDate;
                                Session["Address"] = seeker.Address;
                                return RedirectToAction("MainHome");
                            }
                            else
                            {
                                ModelState.AddModelError(string.Empty, "UserName or Password is Incorrect.");
                            }
                        }
                        else if (user.UserTypeID == 4) // Hospital

                        {
                            var hospital = DB.HospitalTables.Where(u => u.UserID == user.UserID).FirstOrDefault();
                            if (hospital != null)
                            {
                                Session["HospitalID"] = hospital.HospitalD;
                                Session["FullNAme"] = hospital.FullNAme;
                                Session["CityID"] = hospital.CityID;
                                Session["City"] = hospital.CityTable.City;
                                Session["PhoneNo"] = hospital.PhoneNo;
                                
                                Session["Website"] = hospital.Website;
                                Session["Email"] = hospital.Email;
                                Session["Location"] = hospital.Location;
                               
                                Session["Address"] = hospital.Address;
                                return RedirectToAction("MainHome");
                            }
                            else
                            {
                                ModelState.AddModelError(string.Empty, "UserName or Password is Incorrect.");
                            }
                        }
                        else if (user.UserTypeID == 5) // BloodBank

                        {
                            var bloodbank = DB.BloodBankTables.Where(u => u.UserID == user.UserID).FirstOrDefault();
                            if (bloodbank != null)
                            {
                                Session["BloodBankID"] = bloodbank.BLoodBankID;
                                Session["BloodBankName"] = bloodbank.BloodBankName;
                                Session["CityID"] = bloodbank.CityID;
                                Session["City"] = bloodbank.CityTable.City;
                                Session["PhoneNo"] = bloodbank.PhoneNo;

                                Session["Website"] = bloodbank.Website;
                                Session["Email"] = bloodbank.Email;
                                Session["Location"] = bloodbank.Location;

                                Session["Address"] = bloodbank.Address;
                                return RedirectToAction("MainHome");
                            }
                            else
                            {
                                ModelState.AddModelError(string.Empty, "UserName or Password is Incorrect.");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "UserName or Password is Incorrect.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "UserName or Password is Incorrect.");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "UserName or Password is Incorrect.");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Please Provide UserName and Password.");
            }
            ClearSession();
            return View(userMV);
        }
        private void ClearSession()
        {
            Session["UserID"] = string.Empty;
            Session["UserName"] = string.Empty;
            Session["Password"] = string.Empty;
            Session["EmailAddress"] = string.Empty;
            Session["AccountStatusID"] = string.Empty;
            Session["AccountStatus"] = string.Empty;
            Session["UserTypeID"] = string.Empty;
            Session["UserType"] = string.Empty;
            Session["Description"] = string.Empty;
        }
        public ActionResult Logout()
        {
            ClearSession();
            return RedirectToAction("MainHome");
        }
    }
}