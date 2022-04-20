using GiellyGreenTeam1.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace GiellyGreenTeam1.Helper
{
    public class JsonResponseHelper
    {
        public static JsonResponse JsonMessage(int ResponseStatus, string Message, dynamic Result)
        {
            var objectResponse = new JsonResponse
            {
                ResponseStatus = ResponseStatus,
                Message = Message,
                Result = Result
            };
            return objectResponse;
        }
    } 
}