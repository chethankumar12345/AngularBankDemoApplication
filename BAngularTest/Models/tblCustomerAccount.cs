//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BAngularTest.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblCustomerAccount
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblCustomerAccount()
        {
            this.tblTransactions = new HashSet<tblTransaction>();
        }
    
        public int CustomerAccountId { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public Nullable<int> AccountTypeId { get; set; }
        public Nullable<System.DateTime> JoinDate { get; set; }
    
        public virtual tblAccountType tblAccountType { get; set; }
        public virtual tblCustomer tblCustomer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblTransaction> tblTransactions { get; set; }
    }
}