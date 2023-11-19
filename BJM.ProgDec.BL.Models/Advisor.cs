using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BJM.ProgDec.BL.Models
{
    public class Advisor
    {
        public int Id { get; set; }
        [DisplayName("Advisor Name")]
        public string Name { get; set; } = string.Empty;
    }
}
