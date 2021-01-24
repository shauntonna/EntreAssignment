using Microsoft.EntityFrameworkCore;
using ShoppingCart.Data.Context;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Data.Repositories
{
    public class CartsItemRepository : ICartItemRepository
    {
        ShoppingCartDbContext _context;
        public CartsItemRepository(ShoppingCartDbContext context)
        {
            _context = context;

        }

        public void addCart(CartItem cartItem)
        {
            _context.CartItems.Add(cartItem);
            _context.SaveChanges();
        }

        public void DeleteCart(CartItem cartItem)
        {
            _context.CartItems.Remove(cartItem);
            _context.SaveChanges();
        }

        public CartItem CartItem(int id)
        {
            return _context.CartItems.FirstOrDefault(z => z.Id == id);
        }

        public IQueryable<CartItem> GetCartItems()
        {
            return _context.CartItems;
        }

        public void Update(CartItem cartItem)
        {
            var Item = _context.CartItems.SingleOrDefault(x => x.Id == cartItem.Id);

            if(Item != null)
            {
                Item.Qty = cartItem.Qty;
                _context.Entry(Item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();

            }
        }
    }
}
