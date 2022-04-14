using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GiellyGreenTeam1.Models
{
    public class SupplierViewModel
    {
        public int SupplierId { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Please enter valid Supplier Name")]
        public string SupplierName { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Please enter valid supplier reference number")]
        public string SupplierReference { get; set; }

        [MaxLength(150)]
        public string BusinessAddress { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([A-Za-z0-9][^'!&\\#*$%^?<>()+=:;`~\[\]{}|/,₹€@ ][a-zA-z0-9-._][^!&\\#*$%^?<>()+=:;`~\[\]{}|/,₹€@ ]*\@[a-zA-Z0-9][^!&@\\#*$%^?<>()+=':;~`.\[\]{}|/,₹€ ]*\.[a-zA-Z]{2,6})$", ErrorMessage = "Please enter valid Email")]
        public string EmailAddress { get; set; }

        [MaxLength(15)]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Only digit allow")]
        public string PhoneNumber { get; set; }

        [MaxLength(15)]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Only digit allow")]
        public string CompanyRegisterNumber { get; set; }

        [MaxLength(15)]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Only digit allow")]
        public string VATNumber { get; set; }

        [MaxLength(15)]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Only digit allow")]
        public string TaxReference { get; set; }

        [MaxLength(150)]
        public string CompanyRegisterAddress { get; set; }

        public byte[] logo { get; set; }

        public Nullable<bool> Isactive { get; set; }
    }
}