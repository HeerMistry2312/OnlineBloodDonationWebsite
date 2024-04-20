//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DatabaseLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class DonorTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DonorTable()
        {
            this.BloodBankStockDetailTables = new HashSet<BloodBankStockDetailTable>();
        }
    
        public int DonorId { get; set; }
        public string FullName { get; set; }
        public int BloodGroupID { get; set; }
        public System.DateTime LastDonationDate { get; set; }
        public string ContactNo { get; set; }
        public string CNIC { get; set; }
        public int CityID { get; set; }
        public Nullable<int> UserID { get; set; }
        public int GenderID { get; set; }
        public string Location { get; set; }
    
        public virtual BloodGroupsTable BloodGroupsTable { get; set; }
        public virtual CityTable CityTable { get; set; }
        public virtual UserTable UserTable { get; set; }
        public virtual GenderTable GenderTable { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BloodBankStockDetailTable> BloodBankStockDetailTables { get; set; }
    }
}
