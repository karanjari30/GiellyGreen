using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace GiellyGreenTeam1.Models
{
    public class SupplierViewModel
    {
        public int SupplierId;

        private string _supplierName { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Please enter valid Supplier Name")]
        public string SupplierName
        {
            get { return _supplierName; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    _supplierName = value.Trim();
                    _supplierName = Regex.Replace(SupplierName, @"\s+", " ");
                }
                else
                    _supplierName = value;
            }
        }

        private string _supplierReference;
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Please enter valid supplier reference number")]
        [MaxLength(15)]
        public string SupplierReference
        {
            get { return _supplierReference; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    _supplierReference = value.Trim();
                else
                    _supplierReference = value;
            }
        }

        private string _businessAddress;
        [MaxLength(150)]
        public string BusinessAddress
        {
            get { return _businessAddress; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    _businessAddress = value.Trim();
                    _businessAddress = Regex.Replace(BusinessAddress, @"\s+", " ");
                }
                else
                    _businessAddress = value;
            }
        }

        private string _email;
        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([A-Za-z0-9][^'!&\\#*$%^?<>()+=:;`~\[\]{}|/,₹€@ ][a-zA-z0-9-._][^!&\\#*$%^?<>()+=:;`~\[\]{}|/,₹€@ ]*\@[a-zA-Z0-9][^!&@\\#*$%^?<>()+=':;~`.\[\]{}|/,₹€ ]*\.[a-zA-Z]{2,6})$", ErrorMessage = "Please enter valid Email")]
        public string EmailAddress
        {
            get { return _email; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    _email = value.Trim();
                else
                    _email = value;
            }
        }

        private string _phoneNumber;
        [MaxLength(15)]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Only digit allow")]
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    _phoneNumber = value.Trim();
                else
                    _phoneNumber = value;
            }
        }

        private string _companyRegisterNumber;
        [MaxLength(15)]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Only digit allow")]
        public string CompanyRegisterNumber
        {
            get { return _companyRegisterNumber; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    _companyRegisterNumber = value.Trim();
                else
                    _companyRegisterNumber = value;
            }
        }

        private string _VATNumber;
        [MaxLength(15)]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Please enter valid Vat number number")]
        public string VATNumber
        {
            get { return _VATNumber; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    _VATNumber = value.Trim();
                else
                    _VATNumber = value;
            }
        }

        private string _taxReference;
        [MaxLength(15)]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Please enter valid Tax reference number")]
        public string TaxReference
        {
            get { return _taxReference; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    _taxReference = value.Trim();
                else
                    _taxReference = value;
            }
        }

        private string _companyRegisterAddress;
        [MaxLength(150)]
        public string CompanyRegisterAddress
        {
            get { return _companyRegisterAddress; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    _companyRegisterAddress = value.Trim();
                    _companyRegisterAddress = Regex.Replace(CompanyRegisterAddress, @"\s+", " ");
                }
                else
                    _companyRegisterAddress = value;
            }
        }

        public string logo { get; set; }

        public Nullable<bool> Isactive { get; set; }

    }
}