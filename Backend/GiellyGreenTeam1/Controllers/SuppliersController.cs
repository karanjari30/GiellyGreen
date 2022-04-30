using System;
using System.IO;
using System.Linq;
using System.Web;
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
    public class SuppliersController : ApiController
    {
        public GiellyGreen_Team1Entities objSuppiler = new GiellyGreen_Team1Entities();
        static readonly ISupplierRepository supplierRepository = new SupplierRepository();
        public dynamic config = new MapperConfiguration(cfg => cfg.CreateMap<SupplierViewModel, Supplier>());
        public JsonResponse GetSuppliers()
        {
            var objResponse = new JsonResponse();
            try
            {
                var objSuppilerlists = supplierRepository.GetSuppliers();
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

        [HttpPost]
        public JsonResponse PostSupplier(SupplierViewModel model)
        {
            var objResponse = new JsonResponse();
            try
            {
                if (ModelState.IsValid)
                {
                    model.logo = LogoHelper.StoreLogoInFileSystem(model.logo, model.SupplierName);
                    var Suppliermapper = config.CreateMapper().Map<Supplier>(model);
                    supplierRepository.PostSupplier(Suppliermapper);
                    objResponse = JsonResponseHelper.JsonMessage(1, "Record Created Successfully", Suppliermapper);
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

        public JsonResponse PutSupplier(int id, SupplierViewModel model)
        {
            var objResponse = new JsonResponse();
            try
            {
                if (ModelState.IsValid)
                {
                    if (objSuppiler.Suppliers.Find(id) != null)
                    {
                        model.SupplierId = id;
                        model.logo = LogoHelper.StoreLogoInFileSystem(model.logo, model.SupplierName);
                        var Suppliermapper = config.CreateMapper().Map<Supplier>(model);
                        supplierRepository.PutSupplier(Suppliermapper, id);
                        objResponse = JsonResponseHelper.JsonMessage(1, "Record Updated Successfully", Suppliermapper);
                    }
                    else
                        objResponse = JsonResponseHelper.JsonMessage(2, "No Record found", null);
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

        public JsonResponse DeleteSupplier(int id)
        {
            var objResponse = new JsonResponse();
            try
            {
                var objSuppilerData = supplierRepository.DeleteSupplier(id);
                if (objSuppilerData == 1)
                    objResponse = JsonResponseHelper.JsonMessage(1, "Record deleted successfully.", objSuppilerData);
                else if (objSuppilerData == 2)
                    objResponse = JsonResponseHelper.JsonMessage(1, "Invoice already exist. can't deleted record.", objSuppilerData);
                else
                    objResponse = JsonResponseHelper.JsonMessage(2, "Record Not Found.", null);
            }
            catch (Exception ex)
            {
                objResponse = JsonResponseHelper.JsonMessage(0, "Error.", ex.Message);
            }
            return objResponse;
        }

        public JsonResponse PatchStatus(int id, bool isActive)
        {
            var objResponse = new JsonResponse();
            try
            {
                if (objSuppiler.Suppliers.Find(id) != null)
                    objResponse = JsonResponseHelper.JsonMessage(1, "Status Updated Successfully", objSuppiler.ChangeIsActive(id, isActive));
                else
                    objResponse = JsonResponseHelper.JsonMessage(2, "No Record found", null);
            }
            catch (Exception ex)
            {
                objResponse = JsonResponseHelper.JsonMessage(0, "Error", ex.Message);
            }
            return objResponse;
        }
    }
}