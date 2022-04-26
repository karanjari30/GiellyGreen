using AutoMapper;
using DataAccessLayer.Interface;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Services
{
    public class MonthlyInvoiceRepository : IMonthlyInvoice
    {
        public GiellyGreen_Team1Entities db = new GiellyGreen_Team1Entities();
        public dynamic GetMonthlyInvoices(int month, int year)
        {
            MapperConfiguration configuration = new MapperConfiguration(x => x.CreateMap<IsActive_Result, GetAllInvoice_Result>());
            Mapper mapper = new Mapper(configuration);
            var objInvoicelists = db.GetAllInvoice(month, year).ToList();
            var objActiveSupplier = db.IsActive().ToList();

            foreach (var objsupplier in objActiveSupplier)
            {
                if (objInvoicelists.Where(x => x.SupplierId == objsupplier.SupplierId).FirstOrDefault() == null)
                    objInvoicelists.Add(mapper.Map<GetAllInvoice_Result>(objsupplier));
            }
            return objInvoicelists;
        }

        public dynamic PostMonthlyInvoice(dynamic model)
        {
            dynamic objectMonthlyInvoice = "";
            var objectInvoice = Enumerable.FirstOrDefault(db.InsertUpdateInvoice(0, model.Custom1, model.Custom2, model.Custom3, model.Custom4, model.Custom5, model.InvoiceReferenceId, model.InvoiceYear, model.InvoiceMonth, model.InvoiceDate)).Id;
            foreach (var item in model.InvoiceViewList)
            {
                if (item.NetAmount > 0)
                    objectMonthlyInvoice = db.InsertUpdateMonthlyInvoice(0, item.HairService, item.BeautyService, item.CustomHeader1, item.CustomHeader2, item.CustomHeader3, item.CustomHeader4, item.CustomHeader5, item.NetAmount, item.VATAmount, item.GrossAmount, item.AdvancePay, item.BalanceDue, item.IsApprove, item.SupplierId, objectInvoice);
            }
            return objectMonthlyInvoice;
        }

        public dynamic PatchApproveSupplier(List<int> id, int month, int year)
        {
            dynamic supplierObject = "";
            if (id != null)
            {
                foreach (int item in id)
                {
                    supplierObject = db.ApproveSupplier(item, month, year);
                }
            }
            return supplierObject;
        }
    }
}
