using DataAccessLayer.Model;
using GiellyGreenTeam1.Helper;
using GiellyGreenTeam1.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace GiellyGreenTeam1.Controllers
{
    public class PDFController : ApiController
    {
        public GiellyGreen_Team1Entities db = new GiellyGreen_Team1Entities();
        public JsonResponse Pdf(int[] ids, int month, int year)
        {
            var objResponse = new JsonResponse();
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
                string base64Pdf = Convert.ToBase64String(controller.getCombinePDF(supplierLIstForPdf));
                objResponse = JsonResponseHelper.JsonMessage(1, "Successfully send mail", base64Pdf);
            }
            catch(Exception ex)
            {
                objResponse = JsonResponseHelper.JsonMessage(0, "Error", ex.Message);
            }

            return objResponse;
        }
    }
}
