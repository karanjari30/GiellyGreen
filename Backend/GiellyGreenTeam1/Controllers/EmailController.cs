using DataAccessLayer.Model;
using GiellyGreenTeam1.Helper;
using GiellyGreenTeam1.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace GiellyGreenTeam1.Controllers
{
    [System.Web.Http.Authorize]
    public class EmailController : ApiController
    {
        public GiellyGreen_Team1Entities db = new GiellyGreen_Team1Entities();
        public JsonResponse SendEmail(int[] ids, int month, int year)
        {
            var objResponse = new JsonResponse();
            PdfProfile profile = new PdfProfile();
            try
            {
                var supplierLIstForPdf = db.GetSupplierInvoiceForPDF(String.Join(",", ids), month, year).ToList();
                HomeController controller = new HomeController();
                RouteData route = new RouteData();
                route.Values.Add("action", "RenderRazorViewToString"); // ActionName
                route.Values.Add("controller", "Home"); // Controller Name
                ControllerContext newContext = new ControllerContext(new HttpContextWrapper(HttpContext.Current), route, controller);
                controller.ControllerContext = newContext;

                if (supplierLIstForPdf.Count > 0)
                {
                    foreach (var invoice in supplierLIstForPdf)
                    {
                        if (invoice.NetAmount > 0)
                        {
                            if (invoice.logo != null && invoice.logo != "")
                                invoice.logo = Path.Combine(HttpContext.Current.Server.MapPath("~/ImageStorage"), invoice.logo);

                            profile.getSupplierInvoiceForPDF_Result = invoice;
                            profile.companyProfile = db.GetCompanyProfile().FirstOrDefault();
                            string viewstring = controller.RenderRazorViewToString("~/Views/Home/Pdf.cshtml", profile);
                            var base64String = HtmlToPdfHelper.Base64Encode(viewstring);
                            var result = HtmlToPdfHelper.GetByteData(new HtmlToPdf() { FileName = "demo.pdf", HtmlData = new List<string>() { base64String } }).Replace('"', ' ').Trim();
                            Attachment attPDF = new Attachment(new MemoryStream(Convert.FromBase64String(result)), invoice.SupplierName + "_Invoice.pdf");
                            EmailHelper.SendEmailToSupplier(invoice.EmailAddress, month, year, attPDF);
                        }
                    }
                    objResponse = JsonResponseHelper.JsonMessage(1, "Successfully send mail", 1);
                }
                else
                    objResponse = JsonResponseHelper.JsonMessage(0, "No Record Found", null);
            }
            catch (Exception ex)
            {
                objResponse = JsonResponseHelper.JsonMessage(0, "Error", ex.Message);
            }
            return objResponse;
        }
    }
}