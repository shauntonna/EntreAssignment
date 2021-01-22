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
        public IQueryable<Product> GetProducts()
        {
            return _context.Products;
        }
        public Guid AddProduct(Product p)
        {
            
            _context.Products.Add(p);
            _context.SaveChanges();
            return p.id;
        }
    }
}
