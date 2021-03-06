//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccessLayer.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Supplier
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Supplier()
        {
            this.MonthlyInvoices = new HashSet<MonthlyInvoice>();
        }
    
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string SupplierReference { get; set; }
        public string BusinessAddress { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string CompanyRegisterNumber { get; set; }
        public string VATNumber { get; set; }
        public string TaxReference { get; set; }
        public string CompanyRegisterAddress { get; set; }
        public string logo { get; set; }
        public Nullable<bool> Isactive { get; set; }
        public Nullable<bool> IsInvoice { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MonthlyInvoice> MonthlyInvoices { get; set; }
    }
}
