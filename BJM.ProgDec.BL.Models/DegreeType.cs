using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BJM.ProgDec.BL.Models
{
    public class DegreeType
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public List<Program> Programs { get; set; } = new List<Program>();
    }
}
