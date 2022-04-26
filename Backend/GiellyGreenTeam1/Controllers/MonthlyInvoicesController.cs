using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using DataAccessLayer.Interface;
using DataAccessLayer.Model;
using DataAccessLayer.Services;
using GiellyGreenTeam1.Helper;
using GiellyGreenTeam1.Models;

namespace GiellyGreenTeam1.Controllers
{
    [Authorize]
    public class MonthlyInvoicesController : ApiController
    {
        public GiellyGreen_Team1Entities db = new GiellyGreen_Team1Entities();
        static readonly IMonthlyInvoice monthlyInvoice = new MonthlyInvoiceRepository();
        public JsonResponse GetMonthlyInvoices(int month, int year)
        {
            var objResponse = new JsonResponse();
            try
            {
                var objInvoicelists = monthlyInvoice.GetMonthlyInvoices(month, year);
                if (objInvoicelists != null)
                    objResponse = JsonResponseHelper.JsonMessage(1, "Total " + objInvoicelists.Count + " Record found", objInvoicelists);
                else
                    objResponse = JsonResponseHelper.JsonMessage(1, "Record Not Found.", null);
            }
            catch (Exception ex)
            {
                objResponse = JsonResponseHelper.JsonMessage(0, ex.Message, null);
            }
            return objResponse;
        }

        public JsonResponse PostMonthlyInvoice(InvoiceViewModel model)
        {
            var objResponse = new JsonResponse();
            try
            {
                if (ModelState.IsValid)
                {
                    var objectMonthlyInvoice = monthlyInvoice.PostMonthlyInvoice(model);
                   
                    if (objectMonthlyInvoice != null)
                        objResponse = JsonResponseHelper.JsonMessage(1, "Record Save Successfully", objectMonthlyInvoice);
                }
                else
                    objResponse = JsonResponseHelper.JsonMessage(0, "Error", ModelState.Values.SelectMany(E => E.Errors).Select(E => E.ErrorMessage).ToList());
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message != null)
                    objResponse = JsonResponseHelper.JsonMessage(0, "Error", ex.InnerException.Message);
                else
                    objResponse = JsonResponseHelper.JsonMessage(0, "Error", ex.Message);
            }
            return objResponse;
        }

        public JsonResponse PatchApproveSupplier(List<int> id, int month, int year)
        {
            var objResponse = new JsonResponse();
            try
            {
                if(id != null)
                {
                    var supplierObject = monthlyInvoice.PatchApproveSupplier(id, month, year);
                    if (supplierObject == 1)
                        objResponse = JsonResponseHelper.JsonMessage(1, "Approve Supplier Successfully", supplierObject);
                    else
                        objResponse = JsonResponseHelper.JsonMessage(2, "No supplier found for Approve", supplierObject);
                }
            }
            catch (Exception ex)
            {
                objResponse = JsonResponseHelper.JsonMessage(0, "Error", ex.Message);
            }
            return objResponse;
        }
    }
}