using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ItSerwis.Model
{
    public class Vendor
    {
        [Key]
        public int VendorID { get; set; }
        public string BusinessName { get; set; }
        public string ContactName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Voivodeship { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Notes { get; set; }
        public string Fax { get; set; }
        public string Mobile { get; set; }
        public DateTime SubmittedDate { get; set; }

    }
}
