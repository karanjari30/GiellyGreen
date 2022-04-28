using GiellyGreenTeam1.Models;
using System;
using System.IO;
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
            return new Rotativa.ViewAsPdf("~/Views/Home/CombineInvoicePdf.cshtml", list).BuildFile(ControllerContext); ;
        }

        public string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }
    }
}
