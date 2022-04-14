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
        private GiellyGreen_Team1Entities objSuppiler = new GiellyGreen_Team1Entities();

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

        // PUT: api/Suppliers/5
        public IHttpActionResult PutSupplier(int id, Supplier supplier)
        {
            if (!ModelState.IsValid)
            {
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Suppliers
        public IHttpActionResult PostSupplier(Supplier supplier)
        {
            if (!ModelState.IsValid)
            {
              
            }
            return CreatedAtRoute("DefaultApi", new { id = supplier.SupplierId }, supplier);
        }

        // DELETE: api/Suppliers/5
        [ResponseType(typeof(Supplier))]
        public IHttpActionResult DeleteSupplier(int id)
        {
            Supplier supplier = objSuppiler.Suppliers.Find(id);
            if (supplier == null)
            {
                return NotFound();
            }

            objSuppiler.Suppliers.Remove(supplier);
            objSuppiler.SaveChanges();

            return Ok(supplier);
        }

        private bool SupplierExists(int id)
        {
            return objSuppiler.Suppliers.Count(e => e.SupplierId == id) > 0;
        }
    }
}