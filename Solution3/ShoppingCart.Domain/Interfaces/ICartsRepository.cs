using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Domain.Interfaces
{
    public interface ICartsRepository
    {

        
        int AddProduct(Cart cart);

        Cart userCart(string email);

        void delete(Cart cart);

        void update(Cart cart);
    }
}
