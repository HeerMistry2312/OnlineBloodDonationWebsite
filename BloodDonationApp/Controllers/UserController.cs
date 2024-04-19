using BloodDonationApp.Models;
using DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BloodDonationApp.Controllers
{
    public class UserController : Controller
    {
        ByteBridgeDbEntities DB = new ByteBridgeDbEntities();
        // GET: User
        public ActionResult UserProfile(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var user = DB.UserTables.Find(id);
            return View(user);
        }
        public ActionResult EditUserProfile(int? id)
        {
            
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
           
            var userprofile = new RegistrationMV();
            var user = DB.UserTables.Find(id);
            userprofile.UserTypeID = user.UserTypeID;

            userprofile.User.UserID = user.UserID;
                userprofile.User.UserName = user.UserName;
               
            userprofile.User.EmailAddress = user.EmailAddress;
            userprofile.User.AccountStatusID = user.AccountStatusID;
               
                userprofile.User.UserTypeID = user.UserTypeID;
                
                userprofile.User.Description = user.Description;
                
            if (user.SeekerTables.Count > 0)
            {
                var seeker = user.SeekerTables.FirstOrDefault();
                userprofile.Seeker.SeekerID = seeker.SeekerID;
                userprofile.Seeker.FullName = seeker.FullName;

                userprofile.Seeker.Age = seeker.Age;
                userprofile.Seeker.CityID = seeker.CityID;

                userprofile.Seeker.BloodGroupID  = seeker.BloodGroupID;

                userprofile.Seeker.ContactNo = seeker.ContactNo;
                userprofile.Seeker.CNIC = seeker.CNIC;
                userprofile.Seeker.GenderID = seeker.GenderID;
             
                userprofile.Seeker.Address  = seeker.Address;
                userprofile.Seeker.UserID = seeker.UserID;
                userprofile.ContactNo = seeker.ContactNo;
                userprofile.CityID = seeker.CityID;
                userprofile.BloodGroupID = seeker.BloodGroupID;
                userprofile.GenderID = seeker.GenderID;

            }
            else if (user.HospitalTables.Count > 0)
            {
                var hospital = user.HospitalTables.FirstOrDefault();
                userprofile.Hospital.HospitalD =hospital.HospitalD;
                userprofile.Hospital.FullNAme = hospital.FullNAme;
                                                
                userprofile.Hospital.Address = hospital.Address;
                userprofile.Hospital.PhoneNo = hospital.PhoneNo;
                                                
                userprofile.Hospital.Website = hospital.Website;
                                               
                userprofile.Hospital.Email = hospital.Email;
                userprofile.Hospital.Location = hospital.Location;
                userprofile.Hospital.CityID = hospital.CityID;
                userprofile.Hospital.UserID = hospital.UserID;
                userprofile.ContactNo = hospital.PhoneNo;
                userprofile.CityID = hospital.CityID;
                

            }
            else if (user.BloodBankTables.Count > 0)
            {
                var bloodbank = user.BloodBankTables.FirstOrDefault();
                userprofile.BloodBank.BLoodBankID = bloodbank.BLoodBankID;
                userprofile.BloodBank.BloodBankName = bloodbank.BloodBankName;
                userprofile.BloodBank.Address = bloodbank.Address;
                userprofile.BloodBank.PhoneNo = bloodbank.PhoneNo;
                userprofile.BloodBank.Website = bloodbank.Website;
                userprofile.BloodBank.Email = bloodbank.Email;
                userprofile.BloodBank.Location = bloodbank.Location;
                userprofile.BloodBank.CityID = bloodbank.CityID;
                userprofile.BloodBank.UserID = bloodbank.UserID;
                userprofile.ContactNo = bloodbank.PhoneNo;
                userprofile.CityID = bloodbank.CityID;
               

            }
            else if (user.DonorTables.Count > 0)
            {
                var donor = user.DonorTables.FirstOrDefault();
                userprofile.Donor.DonorId = donor.DonorId;
                userprofile.Donor.FullName = donor.FullName;
                userprofile.Donor.GenderID = donor.GenderID;
                userprofile.Donor.BloodGroupID = donor.BloodGroupID;
                userprofile.Donor.LastDonationDate = donor.LastDonationDate;
                userprofile.Donor.ContactNo = donor.ContactNo;
                userprofile.Donor.CNIC = donor.CNIC;
                userprofile.Donor.Location = donor.Location;
                userprofile.Donor.CityID = donor.CityID;
                userprofile.Donor.UserID = donor.UserID;
                userprofile.ContactNo = donor.ContactNo;
                userprofile.CityID = donor.CityID;
                userprofile.BloodGroupID = donor.BloodGroupID;
                userprofile.GenderID = donor.GenderID;
            }
            ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", userprofile.CityID);
            ViewBag.BloodGroupID = new SelectList(DB.BloodGroupsTables.ToList(), "BloodGroupID", "BloodGroup", userprofile.BloodGroupID);
            ViewBag.GenderID = new SelectList(DB.GenderTables.ToList(), "GenderID", "Gender", userprofile.BloodGroupID);
            return View(userprofile);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUserProfile(RegistrationMV userprofile)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            //var user = DB.UserTables.Find(id);
            ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", userprofile.CityID);
            ViewBag.BloodGroupID = new SelectList(DB.BloodGroupsTables.ToList(), "BloodGroupID", "BloodGroup", userprofile.BloodGroupID);
            ViewBag.GenderID = new SelectList(DB.GenderTables.ToList(), "GenderID", "Gender", userprofile.BloodGroupID);
            return View(userprofile);
        }
    }
}