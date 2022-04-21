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
        public void SendEmail(String ToEmail,int month, int year,String companyName)
        {
            var pdf = new ViewAsPdf("Pdf", db.Invoices.ToList());
            HomeController controller = new HomeController();
            RouteData route = new RouteData();
            route.Values.Add("action", "getPDF"); // ActionName
            route.Values.Add("controller", "Home"); // Controller Name
            ControllerContext newContext = new
            ControllerContext(new HttpContextWrapper(HttpContext.Current), route, controller);
            controller.ControllerContext = newContext;
            byte[] applicationPDFData = pdf.BuildPdf(newContext);
            Attachment attPDF = new Attachment(new MemoryStream(applicationPDFData), "Invoice.pdf");
            EmailHelper.SendEmailToSupplier(ToEmail, month, year, companyName, attPDF);
        }
    }
}
