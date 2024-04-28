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
    
    public partial class RequestTable
    {
        public int RequestID { get; set; }
        public System.DateTime RequestDate { get; set; }
        public int RequiredBloodGroupID { get; set; }
        public int RequestTypeID { get; set; }
        public int AcceptedID { get; set; }
        public int RequestByID { get; set; }
        public int AcceptedTypeID { get; set; }
        public string RequestDetails { get; set; }
        public int RequestStatusID { get; set; }
        public System.DateTime ExpectedDate { get; set; }
    
        public virtual AcceptedTypeTable AcceptedTypeTable { get; set; }
        public virtual RequestStatusTable RequestStatusTable { get; set; }
        public virtual RequestTypeTAble RequestTypeTAble { get; set; }
    }
}
