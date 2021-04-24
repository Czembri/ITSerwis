using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ItSerwis.Model
{
    public class Location
    {
        [Key]
        public int LocationID { get; set; }
        public string Name { get; set; }
    }
}
