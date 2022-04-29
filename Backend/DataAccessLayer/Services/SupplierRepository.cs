using DataAccessLayer.Interface;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Services
{
    public class SupplierRepository : ISupplierRepository
    {
        public GiellyGreen_Team1Entities objSuppiler = new GiellyGreen_Team1Entities();

        public dynamic GetSuppliers()
        {
            var objSuppilerlists = objSuppiler.GetSuppliers().ToList();
            String path = HttpContext.Current.Server.MapPath("~/ImageStorage"); //Path Check if directory exist
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path); //Create directory if it doesn't exist

            objSuppilerlists.ForEach(supplier =>
            {
                if (!String.IsNullOrEmpty(supplier.logo) && supplier.logo != "null")
                {
                    try
                    {
                        byte[] imageByte = File.ReadAllBytes(Path.Combine(path, supplier.logo));
                        supplier.logo = Convert.ToBase64String(imageByte);
                    }
                    catch
                    {
                        supplier.logo = Convert.ToBase64String(File.ReadAllBytes(Path.Combine(HttpContext.Current.Server.MapPath(@"~\Content\image\imagenotfound.jpg"))));
                    }
                }
            });
            return objSuppilerlists;
        }

        public Supplier PostSupplier(Supplier model)
        {
            if (model != null)
                objSuppiler.InsertUpdateSupplier(0, model.SupplierName, model.SupplierReference, model.BusinessAddress, model.EmailAddress, model.PhoneNumber, model.CompanyRegisterNumber, model.VATNumber, model.TaxReference, model.CompanyRegisterAddress, model.logo, model.Isactive);
            return model;
        }

        public Supplier PutSupplier(Supplier model, int id)
        {
            if (model != null)
                objSuppiler.InsertUpdateSupplier(id, model.SupplierName, model.SupplierReference, model.BusinessAddress, model.EmailAddress, model.PhoneNumber, model.CompanyRegisterNumber, model.VATNumber, model.TaxReference, model.CompanyRegisterAddress, model.logo, model.Isactive).FirstOrDefault();
            return model;
        }

        public dynamic DeleteSupplier(int id)
        {
            return objSuppiler.DeleteSupplier(id).FirstOrDefault().result;
        }
    }
}
