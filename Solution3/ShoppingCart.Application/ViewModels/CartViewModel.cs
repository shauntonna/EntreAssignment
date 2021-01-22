using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Application.ViewModels
{
    public class CartViewModel
    {
        public Guid id { get; set; }
      
        public int Qty { get; set; }

        public virtual Product Product { get; set; }


        public Guid Product_FK { get; set; }

        public string Email { get; set; }

      
    }
}
