using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodDonationApp.Models
{
    public class BloodBankMV
    {
        public int BLoodBankID { get; set; }
        public string BloodBankName { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public string Location { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }
        public int CityID { get; set; }
        public string City { get; set; }

        public Nullable<int> UserID { get; set; }
        public string UserName { get; set; }
    }
}