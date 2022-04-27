using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GiellyGreenTeam1.Models
{
    public class CombinePdfProfile
    {
        public List<GetSupplierInvoiceForPDF_Result> getSupplierInvoiceForPDF_Result { get; set; }
        public GetCompanyProfile_Result companyProfile { get; set; }
    }
}