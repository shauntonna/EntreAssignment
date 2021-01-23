using ShoppingCart.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Application.Interfaces
{
   public interface ICartsService
    {
        CartViewModel GetCart(string email);

        void addcart(CartViewModel cart);

        void deletecart(CartViewModel cart);

        void update(CartViewModel cart);

    }
}
