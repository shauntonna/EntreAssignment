using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Domain.Interfaces
{
    public interface ICartsRepository
    {

        IQueryable<Product> GetProducts();
        Guid AddProduct(Product p);
    }
}
