using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ItSerwis.Model
{
    public class DeviceBrands
    {
        [Key]
        public int BrandID { get; set; }
        public string Name { get; set; }
    }
}
