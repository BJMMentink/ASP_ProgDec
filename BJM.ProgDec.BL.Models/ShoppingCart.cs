using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BJM.ProgDec.BL.Models
{
    public class ShoppingCart
    {
        const double ITEM_COST = 120.03;
        const double TAX_RATE = .055;

        public List<Declaration> Items {  get; set; } = new List<Declaration>();
        public int NumberOfItems 
        {
            get { return Items.Count; }
        }
        [DisplayFormat(DataFormatString = "{0:C}")] // formats to currency
        public double SubTotal { get { return Items.Count * ITEM_COST; } }
        [DisplayFormat(DataFormatString = "{0:C}")] // formats to currency
        public double Tax { get { return SubTotal * TAX_RATE; } }
        [DisplayFormat(DataFormatString = "{0:C}")] // formats to currency
        public double Total { get { return SubTotal * Tax; } }
    }
}
