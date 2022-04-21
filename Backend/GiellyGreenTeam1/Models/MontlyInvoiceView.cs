using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GiellyGreenTeam1.Models
{
    public class MontlyInvoiceView
    {
        public int? MontlyInvoiceId { get; set; }
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
    }
}