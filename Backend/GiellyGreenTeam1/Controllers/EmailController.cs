using DataAccessLayer.Model;
using GiellyGreenTeam1.Models;
using Rotativa;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace GiellyGreenTeam1.Controllers
{
    public class EmailController : ApiController
    {
        public GiellyGreen_Team1Entities db = new GiellyGreen_Team1Entities();
        public void SendEmail(int[] ids,int month, int year,String companyName)
        {
            var supplierLIstForPdf = db.GetSupplierInvoiceForPDF(String.Join(",", ids), month, year);
            HomeController controller = new HomeController();
            RouteData route = new RouteData();
            route.Values.Add("action", "getPDF"); // ActionName
            route.Values.Add("controller", "Home"); // Controller Name
            ControllerContext newContext = new
            ControllerContext(new HttpContextWrapper(HttpContext.Current), route, controller);
            controller.ControllerContext = newContext;
            foreach(var invoice in supplierLIstForPdf)
            {
                Attachment attPDF = new Attachment(new MemoryStream(controller.getPDF(invoice)), "Invoice.pdf");
                EmailHelper.SendEmailToSupplier(invoice.EmailAddress, month, year, companyName, attPDF);
            }
        }
    }
}
