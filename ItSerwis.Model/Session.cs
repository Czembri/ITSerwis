using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ItSerwis.Model
{
    public class Session
    {
        [Key]
        public int SessionID { get; set; }
        public UserLogin UserID { get; set; } 
        public int Status { get; set; }
        public string GUID { get; set; }
    }
}
