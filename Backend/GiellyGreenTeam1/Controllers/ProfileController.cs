using DataAccessLayer.Model;
using GiellyGreenTeam1.Helper;
using GiellyGreenTeam1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GiellyGreenTeam1.Controllers
{
    public class ProfileController : ApiController
    {
        public GiellyGreen_Team1Entities db = new GiellyGreen_Team1Entities();
        public JsonResponse GetProfile()
        {
            var objectResponse = new JsonResponse();
            try
            {
                var companyProfileList = db.GetCompanyProfile().ToList();
                if (companyProfileList != null && companyProfileList.Count > 0)
                    objectResponse = JsonResponseHelper.JsonMessage(1, "No. of Records " + companyProfileList.Count, companyProfileList);
                else
                    objectResponse = JsonResponseHelper.JsonMessage(2, "No Record Found", companyProfileList);
            }
            catch (Exception ex)
            {
                objectResponse = JsonResponseHelper.JsonMessage(0, "Error", ex.Message);
            }
            return objectResponse;
        }

        [HttpPost]
        public JsonResponse PostProfile(CompanyProfileViewModel model, int? id)
        {
            var objResponse = new JsonResponse();
            try
            {
                if (ModelState.IsValid)
                {
                    var objProfile = db.InsertUpdateCompanyProfile(id, model.CompanyName, model.AddressLine, model.City, model.ZipCode, model.Country, model.DefaultVat);
                    objResponse = JsonResponseHelper.JsonMessage(1, "Record save Successfully", objProfile);
                }
                else
                    objResponse = JsonResponseHelper.JsonMessage(0, "Error", ModelState.Values.SelectMany(E => E.Errors).Select(E => E.ErrorMessage).ToList());
            }
            catch (Exception ex)
            {
                objResponse = JsonResponseHelper.JsonMessage(0, "Error", ex.Message);
            }
            return objResponse;
        }
    }
}
