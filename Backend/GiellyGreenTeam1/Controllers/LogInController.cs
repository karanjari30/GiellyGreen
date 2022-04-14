﻿using GiellyGreenTeam1.Helper;
using GiellyGreenTeam1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GiellyGreenTeam1.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LogInController : ApiController
    {
        public JsonResponse LogIn(LogIn model)
        {
            var objectResponse = new JsonResponse();
            string userName = System.Web.Configuration.WebConfigurationManager.AppSettings["ApiUserName"];
            string userPassword = System.Web.Configuration.WebConfigurationManager.AppSettings["ApiPassword"];
            try
            {
                if (model.Email == userName && model.Password == userPassword)
                {
                    objectResponse = JsonResponseHelper.JsonMessage(1, "Login Successfully", "Welcome " + userName);
                }
                else
                {
                    objectResponse = JsonResponseHelper.JsonMessage(0, "Email and password not match", null);
                }
            }
            catch(Exception ex)
            {
                objectResponse = JsonResponseHelper.JsonMessage(0, "Error", ex.Message);
            }
            return objectResponse;
        }
    }
}
