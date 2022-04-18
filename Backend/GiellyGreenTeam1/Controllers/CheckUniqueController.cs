using DataAccessLayer.Model;
using GiellyGreenTeam1.Helper;
using GiellyGreenTeam1.Models;
using System.Linq;
using System.Web.Http;

namespace GiellyGreenTeam1.Controllers
{
    public class CheckUniqueController : ApiController
    {
        public GiellyGreen_Team1Entities objSuppiler = new GiellyGreen_Team1Entities();

        [Route("CheckUniqueEmailAddress")]
        public JsonResponse IsEmailAddressExist(string EmailAddress, string id = "")
        {
            if (objSuppiler.Suppliers.Any(x => x.EmailAddress == EmailAddress && x.SupplierId.ToString() != id))
                return JsonResponseHelper.JsonMessage(0, "Error", "Email address already exist");
            else
                return JsonResponseHelper.JsonMessage(1, "success", EmailAddress);
        }

        [Route("CheckUniqueSupplierReference")]
        public JsonResponse IsSupplierReferenceExist(string SupplierReference, string id = "")
        {
            if (objSuppiler.Suppliers.Any(x => x.SupplierReference == SupplierReference && x.SupplierId.ToString() != id))
                return JsonResponseHelper.JsonMessage(0, "Error", "Supplier reference already exist");
            else
                return JsonResponseHelper.JsonMessage(1, "success", SupplierReference);
        }

        [Route("CheckUniqueVATNumber")]
        public JsonResponse IsVATNumberExist(string VATNumber, string id = "")
        {
            if (objSuppiler.Suppliers.Any(x => x.VATNumber == VATNumber && x.SupplierId.ToString() != id))
                return JsonResponseHelper.JsonMessage(0, "Error", "VAT number already exist");
            else
                return JsonResponseHelper.JsonMessage(1, "success", VATNumber);
        }

        [Route("CheckUniqueTaxReference")]
        public JsonResponse IsTaxReferenceExist(string TaxReference, string id = "")
        {
            if (objSuppiler.Suppliers.Any(x => x.TaxReference == TaxReference && x.SupplierId.ToString() != id))
                return JsonResponseHelper.JsonMessage(0, "Error", "Tax reference already exist");
            else
                return JsonResponseHelper.JsonMessage(1, "success", TaxReference);
        }
    }
}
