using DataAccessLayer.Model;
using GiellyGreenTeam1.Helper;
using GiellyGreenTeam1.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace GiellyGreenTeam1.Controllers
{
    [System.Web.Http.Authorize]
    public class PDFController : ApiController
    {
        public GiellyGreen_Team1Entities db = new GiellyGreen_Team1Entities();
        public JsonResponse Pdf(int[] ids, int month, int year)
        {
            var objResponse = new JsonResponse();
            CombinePdfProfile combinePdfProfile = new CombinePdfProfile();
            try
            {
                var supplierLIstForPdf = db.GetSupplierInvoiceForPDF(String.Join(",", ids), month, year).ToList();
                HomeController controller = new HomeController();
                RouteData route = new RouteData();
                route.Values.Add("action", "getCombinePDF"); // ActionName
                route.Values.Add("controller", "Home"); // Controller Name
                ControllerContext newContext = new
                ControllerContext(new HttpContextWrapper(HttpContext.Current), route, controller);
                controller.ControllerContext = newContext;

                if(supplierLIstForPdf.Count > 0)
                {
                    combinePdfProfile.getSupplierInvoiceForPDF_Result = supplierLIstForPdf;
                    combinePdfProfile.companyProfile = db.GetCompanyProfile().FirstOrDefault();
                    string base64Pdf = Convert.ToBase64String(controller.getCombinePDF(combinePdfProfile));
                    objResponse = JsonResponseHelper.JsonMessage(1, CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month) + "_" + year + "_Invoices", base64Pdf);
                }
                else
                    objResponse = JsonResponseHelper.JsonMessage(0, "No Records Found", null);
            }
            catch(Exception ex)
            {
                objResponse = JsonResponseHelper.JsonMessage(0, "Error", ex.Message);
            }

            return objResponse;
        }
    }
}
