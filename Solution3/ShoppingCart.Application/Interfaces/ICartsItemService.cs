using ShoppingCart.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Application.Interfaces
{
    public interface ICartsItemService
    {
        CartItemViewModel GetCartItem(int id);

        CartViewModel userCart(string email);

        CartItemViewModel CheckToSee(int id, Guid Product);

        IQueryable<CartItemViewModel> GetCartItems();

        void addCart(CartItemViewModel cartItem);

        void DeleteCart(CartItemViewModel cartItem);

        void Update(CartItemViewModel cartItem);
    }
}
