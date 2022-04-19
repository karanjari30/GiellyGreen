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

        
        // GET: api/MonthlyInvoices
        public JsonResponse GetMonthlyInvoices(int month,int year)
        {
            var objResponse = new JsonResponse();
            try
            {
                object objSuppilerlists;
                if (month == 0 && year == 0) 
                {
                     objSuppilerlists = db.IsActive().ToList();
                }
                else
                {
                     objSuppilerlists = db.GetAllInvoice(month, year).ToList();
                }

                
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

        // POST: api/MonthlyInvoices
        public JsonResponse PostMonthlyInvoice(InvoiceViewModel model)
        {
            var objResponse = new JsonResponse();
            //try
            //{
            //    if (ModelState.IsValid)
            //    {
            //        var objectSupplier = db.InsertUpdateInvoice(0, model.SupplierName, model.SupplierReference, model.BusinessAddress, model.EmailAddress, model.PhoneNumber, model.CompanyRegisterNumber, model.VATNumber, model.TaxReference, model.CompanyRegisterAddress, model.logo, model.Isactive);
            //        if (objectSupplier != null)
            //            objResponse = JsonResponseHelper.JsonMessage(1, "Record Created Successfully", objectSupplier);
            //    }
            //    else
            //        objResponse = JsonResponseHelper.JsonMessage(0, "Error", ModelState.Values.SelectMany(E => E.Errors).Select(E => E.ErrorMessage).ToList());
            //}
            //catch (Exception ex)
            //{
            //    if (ex.InnerException.Message != null)
            //        objResponse = JsonResponseHelper.JsonMessage(0, "Error", ex.InnerException.Message);
            //    else
            //        objResponse = JsonResponseHelper.JsonMessage(0, "Error", ex.Message);
            //}
            return objResponse;
        }

        // PUT: api/MonthlyInvoices/5
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

        

        // DELETE: api/MonthlyInvoices/5
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