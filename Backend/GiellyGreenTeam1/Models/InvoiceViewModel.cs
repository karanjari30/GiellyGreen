using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GiellyGreenTeam1.Models
{
    public class InvoiceViewModel
    {
      
        public int MontlyInvoiceId { get; set; }
        public Nullable<decimal> HairService { get; set; }
        public Nullable<decimal> BeautyService { get; set; }
        public Nullable<decimal> CustomHeader1 { get; set; }
        public Nullable<decimal> CustomHeader2 { get; set; }
        public Nullable<decimal> CustomHeader3 { get; set; }
        public Nullable<decimal> CustomHeader4 { get; set; }
        public Nullable<decimal> CustomHeader5 { get; set; }
        public Nullable<decimal> NetAmount { get; set; }
        public Nullable<decimal> VATAmount { get; set; }
        public Nullable<decimal> GrossAmount { get; set; }
        public Nullable<decimal> AdvancePay { get; set; }
        public Nullable<decimal> BalanceDue { get; set; }
        public Nullable<bool> IsApprove { get; set; }
        public Nullable<int> SupplierId { get; set; }
        public Nullable<int> InvoiceId { get; set; }

        public virtual Invoice Invoice { get; set; }
        public virtual Supplier Supplier { get; set; }
    }

    public partial class Invoice
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Invoice()
        {
            this.MonthlyInvoices = new HashSet<MonthlyInvoice>();
        }

        public int InvoiceId { get; set; }
        public string Custom1 { get; set; }
        public string Custom2 { get; set; }
        public string Custom3 { get; set; }
        public string Custom4 { get; set; }
        public string Custom5 { get; set; }
        public string InvoiceReferenceId { get; set; }
        public Nullable<int> InvoiceYear { get; set; }
        public Nullable<int> InvoiceMonth { get; set; }
        public Nullable<System.DateTime> InvoiceDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MonthlyInvoice> MonthlyInvoices { get; set; }
    }
}