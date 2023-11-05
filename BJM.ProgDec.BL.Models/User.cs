using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BJM.ProgDec.BL.Models
{
    public class User
    {
        public int Id { get; set; }
        [DisplayName("User ID")]
        public string UserId { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; } = string.Empty;
        [DisplayName("Last Name")]
        public string LastName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        [DisplayName("Full Name")]
        public string FullName { get { return FirstName + " " + LastName; } }
    }
}
