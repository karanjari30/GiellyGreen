using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
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
                String path = HttpContext.Current.Server.MapPath("~/ImageStorage"); //Path Check if directory exist
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path); //Create directory if it doesn't exist
                }

                objSuppilerlists.ForEach(supplier =>
                {
                    if (!String.IsNullOrEmpty(supplier.logo) && supplier.logo != "null")
                    {
                        string imgPath = Path.Combine(path, supplier.logo);
                        byte[] imageByte = File.ReadAllBytes(imgPath);
                        supplier.logo = Convert.ToBase64String(imageByte);
                    }
                });

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
                    if (!String.IsNullOrEmpty(model.logo))
                    {
                        String path = HttpContext.Current.Server.MapPath("~/ImageStorage"); //Path
                                                                                            //Check if directory exist
                        if (!System.IO.Directory.Exists(path))
                            System.IO.Directory.CreateDirectory(path); //Create directory if it doesn't exist

                        string imageName = model.SupplierName + ".jpg";
                        //set the image path
                        string imgPath = Path.Combine(path, imageName);
                        byte[] imageBytes = Convert.FromBase64String(model.logo);
                        System.IO.File.WriteAllBytes(imgPath, imageBytes);
                        model.logo = imageName;
                    }

                    var objectSupplier = objSuppiler.InsertUpdateSupplier(0, model.SupplierName, model.SupplierReference, model.BusinessAddress, model.EmailAddress, model.PhoneNumber, model.CompanyRegisterNumber, model.VATNumber, model.TaxReference, model.CompanyRegisterAddress, model.logo, model.Isactive);
                    if (objectSupplier != null)
                        objResponse = JsonResponseHelper.JsonMessage(1, "Record Created Successfully", objectSupplier);
                }
                else
                    objResponse = JsonResponseHelper.JsonMessage(0, "Error", ModelState.Values.SelectMany(E => E.Errors).Select(E => E.ErrorMessage).ToList());
            }
            catch (Exception ex)
            {
                if(ex.InnerException.Message != null)
                    objResponse = JsonResponseHelper.JsonMessage(0, "Error", ex.InnerException.Message.Split('.')[0]);
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
                    if (!String.IsNullOrEmpty(model.logo))
                    {
                        String path = HttpContext.Current.Server.MapPath("~/ImageStorage"); //Path
                                                                                            //Check if directory exist
                        if (!System.IO.Directory.Exists(path))
                            System.IO.Directory.CreateDirectory(path); //Create directory if it doesn't exist

                        string imageName = model.SupplierName + ".jpg";
                        //set the image path
                        string imgPath = Path.Combine(path, imageName);
                        byte[] imageBytes = Convert.FromBase64String(model.logo);
                        System.IO.File.WriteAllBytes(imgPath, imageBytes);
                        model.logo = imageName;
                    }

                    var objectSupplier = objSuppiler.Suppliers.Find(id);
                    if (objectSupplier != null)
                    {
                        var supplierObject = objSuppiler.InsertUpdateSupplier(id, model.SupplierName, model.SupplierReference, model.BusinessAddress, model.EmailAddress, model.PhoneNumber, model.CompanyRegisterNumber, model.VATNumber, model.TaxReference, model.CompanyRegisterAddress, model.logo, model.Isactive).FirstOrDefault();
                        objResponse = JsonResponseHelper.JsonMessage(1, "Record Updated Successfully", supplierObject);
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

        // DELETE: api/Suppliers/5
        public JsonResponse DeleteSupplier(int id)
        {
            var objResponse = new JsonResponse();
            try
            {
                var objSuppilerData = objSuppiler.DeleteSupplier(id).FirstOrDefault().result;
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
                var objectSupplier = objSuppiler.Suppliers.Find(id);
                if (objectSupplier != null)
                {
                    var supplierObject = objSuppiler.ChangeIsActive(id, isActive);
                    objResponse = JsonResponseHelper.JsonMessage(1, "Status Updated Successfully", supplierObject);
                }
                else
                    objResponse = JsonResponseHelper.JsonMessage(2, "No Record found", null);
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