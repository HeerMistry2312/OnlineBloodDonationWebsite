using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BloodDonationApp.Models
{
    public class RequestTypeMV
    {

        public int RequestTypeID { get; set; }
        [Required(ErrorMessage = "*Required")]
        [Display(Name = "Request Type")]
        public string RequestType { get; set; }
    }
}