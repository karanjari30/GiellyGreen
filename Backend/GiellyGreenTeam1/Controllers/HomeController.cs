using DataAccessLayer.Model;
using GiellyGreenTeam1.Models;
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
  
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public Byte[] getPDF(PdfProfile model)
        {
            return new Rotativa.ViewAsPdf("~/Views/Home/Pdf.cshtml", model).BuildFile(ControllerContext); ;
        }

        public Byte[] getCombinePDF(CombinePdfProfile list)
        {
            return new Rotativa.ViewAsPdf("~/Views/Home/getCombinePDF.cshtml", list).BuildFile(ControllerContext); ;
        }
    }
}
