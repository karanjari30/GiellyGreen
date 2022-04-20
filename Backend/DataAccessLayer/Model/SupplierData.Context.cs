﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccessLayer.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class GiellyGreen_Team1Entities : DbContext
    {
        public GiellyGreen_Team1Entities()
            : base("name=GiellyGreen_Team1Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<MonthlyInvoice> MonthlyInvoices { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
    
        public virtual int ChangeIsActive(Nullable<int> id, Nullable<bool> isActive)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            var isActiveParameter = isActive.HasValue ?
                new ObjectParameter("IsActive", isActive) :
                new ObjectParameter("IsActive", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ChangeIsActive", idParameter, isActiveParameter);
        }
    
        public virtual ObjectResult<DeleteSupplier_Result> DeleteSupplier(Nullable<int> id)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<DeleteSupplier_Result>("DeleteSupplier", idParameter);
        }
    
        public virtual ObjectResult<GetAllInvoice_Result> GetAllInvoice(Nullable<int> invoiceMonth, Nullable<int> invoiceYear)
        {
            var invoiceMonthParameter = invoiceMonth.HasValue ?
                new ObjectParameter("InvoiceMonth", invoiceMonth) :
                new ObjectParameter("InvoiceMonth", typeof(int));
    
            var invoiceYearParameter = invoiceYear.HasValue ?
                new ObjectParameter("InvoiceYear", invoiceYear) :
                new ObjectParameter("InvoiceYear", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAllInvoice_Result>("GetAllInvoice", invoiceMonthParameter, invoiceYearParameter);
        }
    
        public virtual ObjectResult<GetSupplierByInvoiceMonth_Result> GetSupplierByInvoiceMonth(Nullable<System.DateTime> invoiceMonth)
        {
            var invoiceMonthParameter = invoiceMonth.HasValue ?
                new ObjectParameter("InvoiceMonth", invoiceMonth) :
                new ObjectParameter("InvoiceMonth", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetSupplierByInvoiceMonth_Result>("GetSupplierByInvoiceMonth", invoiceMonthParameter);
        }
    
        public virtual ObjectResult<GetSuppliers_Result> GetSuppliers()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetSuppliers_Result>("GetSuppliers");
        }
    
        public virtual ObjectResult<InsertUpdateInvoice_Result> InsertUpdateInvoice(Nullable<int> invoiceId, string custom1, string custom2, string custom3, string custom4, string custom5, string invoiceReferenceId, Nullable<int> invoiceYear, Nullable<int> invoiceMonth, Nullable<System.DateTime> invoiceDate)
        {
            var invoiceIdParameter = invoiceId.HasValue ?
                new ObjectParameter("InvoiceId", invoiceId) :
                new ObjectParameter("InvoiceId", typeof(int));
    
            var custom1Parameter = custom1 != null ?
                new ObjectParameter("Custom1", custom1) :
                new ObjectParameter("Custom1", typeof(string));
    
            var custom2Parameter = custom2 != null ?
                new ObjectParameter("Custom2", custom2) :
                new ObjectParameter("Custom2", typeof(string));
    
            var custom3Parameter = custom3 != null ?
                new ObjectParameter("Custom3", custom3) :
                new ObjectParameter("Custom3", typeof(string));
    
            var custom4Parameter = custom4 != null ?
                new ObjectParameter("Custom4", custom4) :
                new ObjectParameter("Custom4", typeof(string));
    
            var custom5Parameter = custom5 != null ?
                new ObjectParameter("Custom5", custom5) :
                new ObjectParameter("Custom5", typeof(string));
    
            var invoiceReferenceIdParameter = invoiceReferenceId != null ?
                new ObjectParameter("InvoiceReferenceId", invoiceReferenceId) :
                new ObjectParameter("InvoiceReferenceId", typeof(string));
    
            var invoiceYearParameter = invoiceYear.HasValue ?
                new ObjectParameter("InvoiceYear", invoiceYear) :
                new ObjectParameter("InvoiceYear", typeof(int));
    
            var invoiceMonthParameter = invoiceMonth.HasValue ?
                new ObjectParameter("InvoiceMonth", invoiceMonth) :
                new ObjectParameter("InvoiceMonth", typeof(int));
    
            var invoiceDateParameter = invoiceDate.HasValue ?
                new ObjectParameter("InvoiceDate", invoiceDate) :
                new ObjectParameter("InvoiceDate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<InsertUpdateInvoice_Result>("InsertUpdateInvoice", invoiceIdParameter, custom1Parameter, custom2Parameter, custom3Parameter, custom4Parameter, custom5Parameter, invoiceReferenceIdParameter, invoiceYearParameter, invoiceMonthParameter, invoiceDateParameter);
        }
    
        public virtual int InsertUpdateMonthlyInvoice(Nullable<int> montlyInvoiceId, Nullable<decimal> hairService, Nullable<decimal> beautyService, Nullable<decimal> customHeader1, Nullable<decimal> customHeader2, Nullable<decimal> customHeader3, Nullable<decimal> customHeader4, Nullable<decimal> customHeader5, Nullable<decimal> netAmount, Nullable<decimal> vATAmount, Nullable<decimal> grossAmount, Nullable<decimal> advancePay, Nullable<decimal> balanceDue, Nullable<bool> isApprove, Nullable<int> supplierId, Nullable<int> invoiceId)
        {
            var montlyInvoiceIdParameter = montlyInvoiceId.HasValue ?
                new ObjectParameter("MontlyInvoiceId", montlyInvoiceId) :
                new ObjectParameter("MontlyInvoiceId", typeof(int));
    
            var hairServiceParameter = hairService.HasValue ?
                new ObjectParameter("HairService", hairService) :
                new ObjectParameter("HairService", typeof(decimal));
    
            var beautyServiceParameter = beautyService.HasValue ?
                new ObjectParameter("BeautyService", beautyService) :
                new ObjectParameter("BeautyService", typeof(decimal));
    
            var customHeader1Parameter = customHeader1.HasValue ?
                new ObjectParameter("CustomHeader1", customHeader1) :
                new ObjectParameter("CustomHeader1", typeof(decimal));
    
            var customHeader2Parameter = customHeader2.HasValue ?
                new ObjectParameter("CustomHeader2", customHeader2) :
                new ObjectParameter("CustomHeader2", typeof(decimal));
    
            var customHeader3Parameter = customHeader3.HasValue ?
                new ObjectParameter("CustomHeader3", customHeader3) :
                new ObjectParameter("CustomHeader3", typeof(decimal));
    
            var customHeader4Parameter = customHeader4.HasValue ?
                new ObjectParameter("CustomHeader4", customHeader4) :
                new ObjectParameter("CustomHeader4", typeof(decimal));
    
            var customHeader5Parameter = customHeader5.HasValue ?
                new ObjectParameter("CustomHeader5", customHeader5) :
                new ObjectParameter("CustomHeader5", typeof(decimal));
    
            var netAmountParameter = netAmount.HasValue ?
                new ObjectParameter("NetAmount", netAmount) :
                new ObjectParameter("NetAmount", typeof(decimal));
    
            var vATAmountParameter = vATAmount.HasValue ?
                new ObjectParameter("VATAmount", vATAmount) :
                new ObjectParameter("VATAmount", typeof(decimal));
    
            var grossAmountParameter = grossAmount.HasValue ?
                new ObjectParameter("GrossAmount", grossAmount) :
                new ObjectParameter("GrossAmount", typeof(decimal));
    
            var advancePayParameter = advancePay.HasValue ?
                new ObjectParameter("AdvancePay", advancePay) :
                new ObjectParameter("AdvancePay", typeof(decimal));
    
            var balanceDueParameter = balanceDue.HasValue ?
                new ObjectParameter("BalanceDue", balanceDue) :
                new ObjectParameter("BalanceDue", typeof(decimal));
    
            var isApproveParameter = isApprove.HasValue ?
                new ObjectParameter("IsApprove", isApprove) :
                new ObjectParameter("IsApprove", typeof(bool));
    
            var supplierIdParameter = supplierId.HasValue ?
                new ObjectParameter("SupplierId", supplierId) :
                new ObjectParameter("SupplierId", typeof(int));
    
            var invoiceIdParameter = invoiceId.HasValue ?
                new ObjectParameter("InvoiceId", invoiceId) :
                new ObjectParameter("InvoiceId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("InsertUpdateMonthlyInvoice", montlyInvoiceIdParameter, hairServiceParameter, beautyServiceParameter, customHeader1Parameter, customHeader2Parameter, customHeader3Parameter, customHeader4Parameter, customHeader5Parameter, netAmountParameter, vATAmountParameter, grossAmountParameter, advancePayParameter, balanceDueParameter, isApproveParameter, supplierIdParameter, invoiceIdParameter);
        }
    
        public virtual ObjectResult<InsertUpdateSupplier_Result> InsertUpdateSupplier(Nullable<int> id, string supplierName, string supplierReference, string businessAddress, string emailAddress, string phoneNumber, string companyRegisterNumber, string vATNumber, string taxReference, string companyRegisterAddress, string logo, Nullable<bool> isactive)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            var supplierNameParameter = supplierName != null ?
                new ObjectParameter("SupplierName", supplierName) :
                new ObjectParameter("SupplierName", typeof(string));
    
            var supplierReferenceParameter = supplierReference != null ?
                new ObjectParameter("SupplierReference", supplierReference) :
                new ObjectParameter("SupplierReference", typeof(string));
    
            var businessAddressParameter = businessAddress != null ?
                new ObjectParameter("BusinessAddress", businessAddress) :
                new ObjectParameter("BusinessAddress", typeof(string));
    
            var emailAddressParameter = emailAddress != null ?
                new ObjectParameter("EmailAddress", emailAddress) :
                new ObjectParameter("EmailAddress", typeof(string));
    
            var phoneNumberParameter = phoneNumber != null ?
                new ObjectParameter("PhoneNumber", phoneNumber) :
                new ObjectParameter("PhoneNumber", typeof(string));
    
            var companyRegisterNumberParameter = companyRegisterNumber != null ?
                new ObjectParameter("CompanyRegisterNumber", companyRegisterNumber) :
                new ObjectParameter("CompanyRegisterNumber", typeof(string));
    
            var vATNumberParameter = vATNumber != null ?
                new ObjectParameter("VATNumber", vATNumber) :
                new ObjectParameter("VATNumber", typeof(string));
    
            var taxReferenceParameter = taxReference != null ?
                new ObjectParameter("TaxReference", taxReference) :
                new ObjectParameter("TaxReference", typeof(string));
    
            var companyRegisterAddressParameter = companyRegisterAddress != null ?
                new ObjectParameter("CompanyRegisterAddress", companyRegisterAddress) :
                new ObjectParameter("CompanyRegisterAddress", typeof(string));
    
            var logoParameter = logo != null ?
                new ObjectParameter("logo", logo) :
                new ObjectParameter("logo", typeof(string));
    
            var isactiveParameter = isactive.HasValue ?
                new ObjectParameter("Isactive", isactive) :
                new ObjectParameter("Isactive", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<InsertUpdateSupplier_Result>("InsertUpdateSupplier", idParameter, supplierNameParameter, supplierReferenceParameter, businessAddressParameter, emailAddressParameter, phoneNumberParameter, companyRegisterNumberParameter, vATNumberParameter, taxReferenceParameter, companyRegisterAddressParameter, logoParameter, isactiveParameter);
        }
    
        public virtual ObjectResult<IsActive_Result> IsActive()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<IsActive_Result>("IsActive");
        }
    
        public virtual int ApproveSupplier(Nullable<int> id, Nullable<int> month, Nullable<int> year)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            var monthParameter = month.HasValue ?
                new ObjectParameter("month", month) :
                new ObjectParameter("month", typeof(int));
    
            var yearParameter = year.HasValue ?
                new ObjectParameter("year", year) :
                new ObjectParameter("year", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ApproveSupplier", idParameter, monthParameter, yearParameter);
        }
    }
}
