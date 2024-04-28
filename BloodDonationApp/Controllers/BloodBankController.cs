using BloodDonationApp.Models;
using DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace BloodDonationApp.Controllers
{
    public class BloodBankController : Controller
    {
        // GET: BloodBank
        ByteBridgeDbEntities DB = new ByteBridgeDbEntities();
        public ActionResult BloodBankStock()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var list = new List<BloodBankStockMV>();
            int bloodbankID = 0;
            int.TryParse(Convert.ToString(Session["BloodBankID"]), out  bloodbankID);
            var stocklist = DB.BloodBankStockTables.Where(b => b.BloodBankID == bloodbankID);
            foreach(var stock in stocklist)
            {
                string bloodbank = stock.BloodBankTable.BloodBankName;
                string bloodgroup = stock.BloodGroupsTable.BloodGroup;
                var bloodBankStockmv = new BloodBankStockMV();
                bloodBankStockmv.BloodBankStockID = stock.BloodBankStockID;
                bloodBankStockmv.BloodBankID = stock.BloodBankID;
                bloodBankStockmv.BloodBank = bloodbank;
                bloodBankStockmv.BloodGroupId = stock.BloodGroupId;
                bloodBankStockmv.BloodGroup = bloodgroup;
                bloodBankStockmv.Quantity = stock.Quantity;
                bloodBankStockmv.Status = stock.Status == true ? "Ready For Use" : "Not Ready";
                bloodBankStockmv.Description = stock.Description;
                list.Add(bloodBankStockmv);

            }

            return View(list);
        }
        public ActionResult AllCampaigns()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            int bloodbankID = 0;
            int.TryParse(Convert.ToString(Session["BloodBankID"]), out bloodbankID);
            var allcampaigns = DB.CampaignTables.Where(c => c.BloodBankID == bloodbankID);
            
            
            return View(allcampaigns);
        }
        public ActionResult NewCampaign()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var campaignMV = new CampaignMV();
            return View(campaignMV);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewCampaign(CampaignMV campaignMV)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            int bloodbankID = 0;
            int.TryParse(Convert.ToString(Session["BloodBankID"]), out bloodbankID);
            campaignMV.BloodBankID = bloodbankID;
            
            if (ModelState.IsValid)
            {
                var campaign = new CampaignTable();
                
                campaign.BloodBankID = bloodbankID;
                campaign.CampaignDate = campaignMV.CampaignDate;
                campaign.StartTime = campaignMV.StartTime;
                campaign.EndTime = campaignMV.EndTime;
                campaign.Location = campaignMV.Location;
                campaign.CampaignDetails = campaignMV.CampaignDetails;
                campaign.CampaignTitle = campaignMV.CampaignTitle;
                campaign.CampaignPhoto = "~/Content/CampaignPhoto/Default.jpg";
                if (DB != null)
                {
                    DB.CampaignTables.Add(campaign);
                    DB.SaveChanges();
                    return RedirectToAction("AllCampaigns");
                }
                else
                {
                    // Handle DB being null, maybe log an error or return an error view
                    return RedirectToAction("Error", "Home");
                }
            }
            


            return View(campaignMV);
        }
    }
}