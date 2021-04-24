using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ItSerwis.Model
{
    public class UserLogin
    {
        [Key]
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string LoginHash { get; set; }
        public string PasswordHash { get; set; }

    }
}
