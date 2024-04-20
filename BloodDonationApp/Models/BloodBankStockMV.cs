using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BloodDonationApp.Models
{
    public class BloodBankStockMV
    {
        public int BloodBankStockID { get; set; }
        public int BloodGroupId { get; set; }
        [Display(Name = "BloodGroup")]
        public string BloodGroup { get; set; }
        [Display(Name ="Is Ready")]
        public string Status { get; set; }
        public int Quantity { get; set; }
        [Display(Name = "Blood Details")]
        public string Description { get; set; }
        public int BloodBankID { get; set; }
        [Display(Name = "BloodBank")]
        public string BloodBank { get; set; }
    }
}