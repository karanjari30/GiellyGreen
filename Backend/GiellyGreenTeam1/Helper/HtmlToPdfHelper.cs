using GiellyGreenTeam1.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GiellyGreenTeam1.Helper
{
    public class HtmlToPdfHelper
    {
        public static string DownloadPDfFromstring(HtmlToPdf htmlToPdf)
        {
            RestResponse restResponse = new RestResponse();
            var client = new RestClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ConvertHtmlToPdfUrl"]);
            var request = new RestRequest("", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(htmlToPdf);
            restResponse = client.PostAsync(request).Result;
            return restResponse.Content;
        }
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}