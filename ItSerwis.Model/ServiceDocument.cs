using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ItSerwis.Model
{
    public class ServiceDocument
    {
        [Key]
        public int ServiceDocumentID { get; set; }
        public DateTime CreationDate { get; set; }
        public string ClientName { get; set; }
        public string ClientLastName { get; set; }
        public string ClientAddress { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeLastName { get; set; }
        public UserLogin UserID { get; set; }
        public string DeviceType { get; set; }
        public DeviceBrands BrandID { get; set; }
        public string DeviceModel { get; set; }
        public string Description { get; set; }

    }
}
