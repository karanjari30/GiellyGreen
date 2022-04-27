using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GiellyGreenTeam1.Models
{
    public class CompanyProfileViewModel
    {
        public int CId { get; set; }

        [Required]
        public string CompanyName { get; set; }
        public string AddressLine { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public Nullable<decimal> DefaultVat { get; set; }
    }
}