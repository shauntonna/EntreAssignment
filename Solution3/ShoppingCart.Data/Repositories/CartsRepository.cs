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
    public class CartsRepository : ICartsRepository
    {
        ShoppingCartDbContext _context;
        public CartsRepository(ShoppingCartDbContext context)
        {
            _context = context;

        }
        public int AddProduct(Cart cart)
        {
            _context.Carts.Add(cart);
            _context.SaveChanges();
            return cart.Id;
        }

        public Cart userCart(string email)
        {
            return _context.Carts.Include(x => x.CartItems).SingleOrDefault(x => x.Email == email);
        }

        public void delete(Cart cart)
        {
            _context.Carts.Remove(cart);
            _context.SaveChanges();
        }

        public void update(Cart cart)
        {
            _context.Entry(cart);
            _context.SaveChanges();
        }
    }
}
