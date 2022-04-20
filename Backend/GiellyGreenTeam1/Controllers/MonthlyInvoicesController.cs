using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Http;
using DataAccessLayer.Model;
using GiellyGreenTeam1.Helper;
using GiellyGreenTeam1.Models;

namespace GiellyGreenTeam1.Controllers
{
    public class MonthlyInvoicesController : ApiController
    {
        public GiellyGreen_Team1Entities db = new GiellyGreen_Team1Entities();

        public JsonResponse GetMonthlyInvoices(int month, int year)
        {
            var objResponse = new JsonResponse();
            try
            {
                var objSuppilerlists = db.GetAllInvoice(month, year).ToList();

                if (objSuppilerlists != null)
                    objResponse = JsonResponseHelper.JsonMessage(1, "Total " + objSuppilerlists.Count + " Record found", objSuppilerlists);
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
                    dynamic objectMonthlyInvoice = "";
                    var objectInvoice = db.InsertUpdateInvoice(0, model.Custom1, model.Custom2, model.Custom3, model.Custom4, model.Custom5, model.InvoiceReferenceId, model.InvoiceYear, model.InvoiceMonth, model.InvoiceDate).FirstOrDefault().Id;
                    foreach (var item in model.InvoiceViewList.ToList())
                    {
                        objectMonthlyInvoice = db.InsertUpdateMonthlyInvoice(0, item.HairService, item.BeautyService, item.CustomHeader1, item.CustomHeader2, item.CustomHeader3, item.CustomHeader4, item.CustomHeader5, item.NetAmount, item.VATAmount, item.GrossAmount, item.AdvancePay, item.BalanceDue, item.IsApprove, item.SupplierId, objectInvoice);
                    }
                    if (objectInvoice != null && objectMonthlyInvoice != null)
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
                dynamic supplierObject = "";
                if(id != null)
                {
                    foreach (int item in id)
                    {
                        supplierObject = db.ApproveSupplier(item, month, year);
                    }
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