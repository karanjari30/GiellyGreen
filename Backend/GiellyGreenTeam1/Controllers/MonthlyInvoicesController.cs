using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DataAccessLayer.Model;
using GiellyGreenTeam1.Helper;
using GiellyGreenTeam1.Models;

namespace GiellyGreenTeam1.Controllers
{
    public class MonthlyInvoicesController : ApiController
    {
        public GiellyGreen_Team1Entities db = new GiellyGreen_Team1Entities();

        public JsonResponse GetMonthlyInvoices(int month,int year)
        {
            var objResponse = new JsonResponse();
            try
            {
                object objSuppilerlists;
                if (month == 0 && year == 0) 
                     objSuppilerlists = db.IsActive().ToList();
                else
                     objSuppilerlists = db.GetAllInvoice(month, year).ToList();

                if (objSuppilerlists != null)
                    objResponse = JsonResponseHelper.JsonMessage(1, "", objSuppilerlists);
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
                    var objInvoiceList = model.InvoiceViewList.ToList().FirstOrDefault();
                    var objectInvoice = db.InsertUpdateInvoice(0, model.Custom1, model.Custom2, model.Custom3, model.Custom4, model.Custom5, model.InvoiceReferenceId, model.InvoiceYear, model.InvoiceMonth, model.InvoiceDate).FirstOrDefault().Id;
                    var objectMonthlyInvoice = db.InsertUpdateMonthlyInvoice(0, objInvoiceList.HairService , objInvoiceList.BeautyService, objInvoiceList.CustomHeader1, objInvoiceList.CustomHeader2, objInvoiceList.CustomHeader3, objInvoiceList.CustomHeader4, objInvoiceList.CustomHeader5, objInvoiceList.NetAmount, objInvoiceList.VATAmount, objInvoiceList.GrossAmount, objInvoiceList.AdvancePay, objInvoiceList.BalanceDue, objInvoiceList.IsApprove, objInvoiceList.SupplierId, objectInvoice);

                    if (objectInvoice != null && objectMonthlyInvoice != null )
                        objResponse = JsonResponseHelper.JsonMessage(1, "Record Created Successfully", objectMonthlyInvoice);
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

        public IHttpActionResult PutMonthlyInvoice(int id, MonthlyInvoice monthlyInvoice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != monthlyInvoice.MontlyInvoiceId)
            {
                return BadRequest();
            }

            db.Entry(monthlyInvoice).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MonthlyInvoiceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        public IHttpActionResult DeleteMonthlyInvoice(int id)
        {
            MonthlyInvoice monthlyInvoice = db.MonthlyInvoices.Find(id);
            if (monthlyInvoice == null)
            {
                return NotFound();
            }

            db.MonthlyInvoices.Remove(monthlyInvoice);
            db.SaveChanges();

            return Ok(monthlyInvoice);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MonthlyInvoiceExists(int id)
        {
            return db.MonthlyInvoices.Count(e => e.MontlyInvoiceId == id) > 0;
        }
    }
}