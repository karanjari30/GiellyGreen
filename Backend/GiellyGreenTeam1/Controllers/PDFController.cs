using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace GiellyGreenTeam1.Controllers
{
    public class PDFController : ApiController
    {
        public HttpResponseMessage Pdf()
        {
            HomeController controller = new HomeController();
            RouteData route = new RouteData();
            route.Values.Add("action", "getPDF"); // ActionName
            route.Values.Add("controller", "Home"); // Controller Name
            System.Web.Mvc.ControllerContext newContext = new
            System.Web.Mvc.ControllerContext(new HttpContextWrapper(System.Web.HttpContext.Current), route, controller);
            controller.ControllerContext = newContext;
            var actionPDF = controller.getPDF();
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new ByteArrayContent(actionPDF);// new StreamContent(new FileStream(localFilePath, FileMode.Open, FileAccess.Read));
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = "SamplePDF.PDF";
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            return response;
        }
    }
}
