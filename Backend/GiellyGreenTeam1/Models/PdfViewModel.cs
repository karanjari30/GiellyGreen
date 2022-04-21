using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GiellyGreenTeam1.Models
{
    public class PdfViewModel
    {
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string BusinessAddress { get; set; }
        public string EmailAddress { get; set; }
        public string logo { get; set; }
        public string TaxReference { get; set; }
        public string VATNumber { get; set; }
        public string Custom1 { get; set; }
        public string Custom2 { get; set; }
        public string Custom3 { get; set; }
        public string Custom4 { get; set; }
        public string Custom5 { get; set; }
        public string InvoiceReferenceId { get; set; }
        public Nullable<int> InvoiceYear { get; set; }
        public Nullable<int> InvoiceMonth { get; set; }
        public Nullable<System.DateTime> InvoiceDate { get; set; }
        public Nullable<int> MontlyInvoiceId { get; set; }
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
        public Nullable<int> SupplierId1 { get; set; }
        public Nullable<int> InvoiceId { get; set; }
    }
}