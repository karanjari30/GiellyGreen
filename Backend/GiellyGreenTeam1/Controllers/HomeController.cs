using DataAccessLayer.Model;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GiellyGreenTeam1.Controllers
{
    public class HomeController : Controller
    {
        public GiellyGreen_Team1Entities db = new GiellyGreen_Team1Entities();
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Pdf()
        {
            return new Rotativa.ViewAsPdf("Pdf", db.Invoices.ToList()) { FileName = "TestViewAsPdf.pdf" };
        }

        public Byte[] getPDF()
        {
            var actionPDF = new Rotativa.ViewAsPdf("Pdf");
            byte[] applicationPDFData = actionPDF.BuildPdf(this.ControllerContext);
            return applicationPDFData;
        }
    }
}
