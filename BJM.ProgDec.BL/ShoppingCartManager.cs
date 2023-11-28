using BJM.ProgDec.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BJM.ProgDec.BL
{
   public static class ShoppingCartManager
    {
        public static void Add(ShoppingCart cart, Declaration declaration)
        {
            if (cart != null)
            {
                cart.Items.Add(declaration);
            }
        }
        public static void Remove(ShoppingCart cart, Declaration declaration)
        {
            if (cart != null)
            {
                cart.Items.Remove(declaration);
            }
        }
        public static void Checkout(ShoppingCart cart)
        {
              // make a new order
              // set order fields as needed
              // foreach(movie item in  cart.Items)

              // Make a new order Item
              // Set the orderItem firelds from the  item
              // order.OrderIterms.Add(OrderItems)

              // order<amager.Insert(order)
              // update the in stk quantity from tblMovie
              // if(tblMovie.count > 0)
            cart = new ShoppingCart();

        }
    }
    
}
