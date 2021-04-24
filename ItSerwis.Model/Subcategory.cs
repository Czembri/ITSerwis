using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ItSerwis.Model
{
    public class Subcategory
    {
        [Key]
        public int SubCatID { get; set; }
        public string SubCatName { get; set; }
        public Category CategoryID { get; set; }
    }
}
