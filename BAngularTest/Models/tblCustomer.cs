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
    
    public partial class tblCustomer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblCustomer()
        {
            this.tblCustomerAccounts = new HashSet<tblCustomerAccount>();
        }
    
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public string EmailId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblCustomerAccount> tblCustomerAccounts { get; set; }
    }
}