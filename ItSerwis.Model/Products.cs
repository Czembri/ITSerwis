using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ItSerwis.Model
{
    public class Products
    {
        [Key]
        public int ProductID { get; set; }
        public string Barcode { get; set; }
        public Location LocationID { get; set; }
        public Subcategory SubCatID { get; set; }
        public Vendor VendorID { get; set; }
        public Taxrate TaxrateID { get; set; }
        public string Description { get; set; }
        public string Weight { get; set; }
        public DateTime LastReceivedDate { get; set; }
        public ItemType ItemTypeID { get; set; }
        public int WarrantyReturns { get; set; }
        public int ActivityStatus { get; set; }
    }
}
