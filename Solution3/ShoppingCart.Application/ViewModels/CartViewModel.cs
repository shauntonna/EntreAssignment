using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Application.ViewModels
{
    public class CartViewModel
    {
        public int id { get; set; }
      
        public double Price { get; set; }

        public string Email { get; set; }

        public IEnumerable<CartItemViewModel> CartItems { get; set; }
    }
}
