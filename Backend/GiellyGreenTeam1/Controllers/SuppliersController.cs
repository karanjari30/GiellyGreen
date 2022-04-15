using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using DataAccessLayer.Model;
using GiellyGreenTeam1.Helper;
using GiellyGreenTeam1.Models;

namespace GiellyGreenTeam1.Controllers
{
    public class SuppliersController : ApiController
    {
        public GiellyGreen_Team1Entities objSuppiler = new GiellyGreen_Team1Entities();

        // GET: api/Suppliers
        public JsonResponse GetSuppliers()
        {
            var objResponse = new JsonResponse();
            try
            {
                var objSuppilerlists = objSuppiler.GetSuppliers().ToList();
                if (objSuppilerlists != null)
                    objResponse = JsonResponseHelper.JsonMessage(1, "Total " + objSuppilerlists.Count + " Record Found.", objSuppilerlists);
                else
                    objResponse = JsonResponseHelper.JsonMessage(1, "Record Not Found.", null);
            }
            catch (Exception ex)
            {
                objResponse = JsonResponseHelper.JsonMessage(0, ex.Message, null);
            }
            return objResponse;
        }
        // POST: api/Suppliers
        [HttpPost]
        public JsonResponse PostSupplier(SupplierViewModel model)
        {
            var objResponse = new JsonResponse();
            try
            {
                if (ModelState.IsValid)
                {
                    var objectSupplier = objSuppiler.InsertUpdateSupplier(0, model.SupplierName, model.SupplierReference, model.BusinessAddress, model.EmailAddress, model.PhoneNumber, model.CompanyRegisterNumber, model.VATNumber, model.TaxReference, model.CompanyRegisterAddress, model.logo, model.Isactive);
                    if (objectSupplier != null)
                    {
                        objResponse = JsonResponseHelper.JsonMessage(1, "Record Created Successfully", objectSupplier);
                    }
                }
                else
                {
                    var allError = ModelState.Values.SelectMany(v => v.Errors);
                    objResponse = JsonResponseHelper.JsonMessage(0, "Error", allError);
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message != null)
                {
                    objResponse = JsonResponseHelper.JsonMessage(0, "Error", ex.InnerException.Message);
                }
                else
                {
                    objResponse = JsonResponseHelper.JsonMessage(0, "Error", ex.Message);
                }
            }
            return objResponse;
        }

        public JsonResponse PutSupplier(int id, SupplierViewModel model)
        {
            var objResponse = new JsonResponse();

            try
            {
                if (ModelState.IsValid)
                {
                    var objectSupplier = objSuppiler.Suppliers.Find(id);
                    if (objectSupplier != null)
                    {
                        var supplierObject = objSuppiler.InsertUpdateSupplier(id, model.SupplierName, model.SupplierReference, model.BusinessAddress, model.EmailAddress, model.PhoneNumber, model.CompanyRegisterNumber, model.VATNumber, model.TaxReference, model.CompanyRegisterAddress, model.logo, model.Isactive).FirstOrDefault();
                        objResponse = JsonResponseHelper.JsonMessage(1, "Record Updated Successfully", supplierObject);
                    }
                    else
                    {
                        objResponse = JsonResponseHelper.JsonMessage(2, "No Record found", null);
                    }
                }
                else
                {
                    var allError = ModelState.Values.SelectMany(v => v.Errors);
                    objResponse = JsonResponseHelper.JsonMessage(0, "Error", allError);
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message != null)
                {
                    objResponse = JsonResponseHelper.JsonMessage(0, "Error", ex.InnerException.Message);
                }
                else
                {
                    objResponse = JsonResponseHelper.JsonMessage(0, "Error", ex.Message);
                }
            }
            return objResponse;
        }

        // DELETE: api/Suppliers/5
        public JsonResponse DeleteSupplier(int id)
        {
            var objResponse = new JsonResponse();
            try
            {
                var objSuppilerData = objSuppiler.DeleteSupplier(id);
                if (objSuppilerData == 1)
                    objResponse = JsonResponseHelper.JsonMessage(1, "Record deleted successfully.", objSuppilerData);
                else
                    objResponse = JsonResponseHelper.JsonMessage(1, "Record Not Found.", null);
            }
            catch (Exception)
            {
                objResponse = JsonResponseHelper.JsonMessage(0, "Error.", ModelState.Values.SelectMany(x => x.Errors));
            }
            return objResponse;
        }

        public JsonResponse PatchStatus(int id, bool isActive)
        {
            var objResponse = new JsonResponse();
            try
            {
                var objectSupplier = objSuppiler.Suppliers.Find(id);
                if (objectSupplier != null)
                {
                    var supplierObject = objSuppiler.ChangeIsActive(id,isActive );
                    objResponse = JsonResponseHelper.JsonMessage(1, "Status Updated Successfully", supplierObject);
                }
                else
                {
                    objResponse = JsonResponseHelper.JsonMessage(2, "No Record found", null);
                }
            }
            catch (Exception ex)
            {
                objResponse = JsonResponseHelper.JsonMessage(0, "Error", ex.Message);
            }
            return objResponse;
        }


        private bool SupplierExists(int id)
        {
            return objSuppiler.Suppliers.Count(e => e.SupplierId == id) > 0;
        }
    }
}