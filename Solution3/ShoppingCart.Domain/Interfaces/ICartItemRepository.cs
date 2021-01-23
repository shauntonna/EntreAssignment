using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Domain.Interfaces
{
    public interface ICartItemRepository
    {
        CartItem CartItem(int id);

        IQueryable<CartItem> GetCartItems();

        void addCart(CartItem cartItem);

        void DeleteCart(CartItem cartItem);

        void Update(CartItem cartItem);

    }
}
