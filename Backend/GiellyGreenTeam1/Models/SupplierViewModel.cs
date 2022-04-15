using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace GiellyGreenTeam1.Models
{
    public class SupplierViewModel
    {
        public int SupplierId { get; set; }

        private string _supplierName { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Please enter valid Supplier Name")]
        public string SupplierName
        {
            get { return _supplierName; } 
            set { _supplierName = value.Trim();
                  _supplierName = Regex.Replace(SupplierName, @"\s+", " ");
                }
        }

        private string _supplierReference { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Please enter valid supplier reference number")]
        [MaxLength(15)]
        public string SupplierReference 
        {
            get { return _supplierReference; }
            set
            {
                _supplierReference = value.Trim();
            }
        }

        private string _businessAddress { get; set; }
        [MaxLength(150)]
        public string BusinessAddress 
        {
            get { return _businessAddress; }
            set
            {
                _businessAddress = value.Trim();
                _businessAddress = Regex.Replace(BusinessAddress, @"\s+", " ");
            }
        }

        private string _email { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([A-Za-z0-9][^'!&\\#*$%^?<>()+=:;`~\[\]{}|/,₹€@ ][a-zA-z0-9-._][^!&\\#*$%^?<>()+=:;`~\[\]{}|/,₹€@ ]*\@[a-zA-Z0-9][^!&@\\#*$%^?<>()+=':;~`.\[\]{}|/,₹€ ]*\.[a-zA-Z]{2,6})$", ErrorMessage = "Please enter valid Email")]
        public string EmailAddress
        {
            get { return _email; }
            set
            {
                _email = value.Trim();
            }
        }

        private string _phoneNumber { get; set; }
        [MaxLength(15)]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Only digit allow")]
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                _phoneNumber = value.Trim();
            }
        }

        private string _companyRegisterNumber { get; set; }
        [MaxLength(15)]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Only digit allow")]
        public string CompanyRegisterNumber 
        {
            get { return _companyRegisterNumber; }
            set
            {
                _companyRegisterNumber = value.Trim();
            }
        }

        private string _VATNumber { get; set; }
        [MaxLength(15)]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Only digit allow")]
        public string VATNumber 
        {
            get { return _VATNumber; }
            set
            {
                _VATNumber = value.Trim();
            }
        }

        private string _taxReference { get; set; }
        [MaxLength(15)]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Please enter valid Tax reference number")]
        public string TaxReference 
        {
            get { return _taxReference; }
            set
            {
                _taxReference = value.Trim();
            }
        }

        private string _companyRegisterAddress { get; set; }
        [MaxLength(150)]
        public string CompanyRegisterAddress
        {
            get { return _companyRegisterAddress; }
            set
            {
                _companyRegisterAddress = value.Trim();
                _companyRegisterAddress = Regex.Replace(CompanyRegisterAddress, @"\s+", " ");
            }
        }

        public byte[] logo { get; set; }

        public Nullable<bool> Isactive { get; set; }
    }
}