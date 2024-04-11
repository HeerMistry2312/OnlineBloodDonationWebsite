using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodDonationApp.Models
{
    public class DonorMV
    {
        public int DonorId { get; set; }
        public string FullName { get; set; }
        public int BloodGroupID { get; set; }
        public string BloodGroup { get; set; }
        public System.DateTime LastDonationDate { get; set; }
        public string ContactNo { get; set; }
        public string CNIC { get; set; }
        public int CityID { get; set; }
        public string City { get; set; }
        public Nullable<int> UserID { get; set; }
        public string UserName { get; set; }

    }
}