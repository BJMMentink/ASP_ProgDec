using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BJM.Bands.UI.Models
{
    public class Band
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public DateTime DateFounded { get; set; }
        public string Image { get; set; }
    }
}
