using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.ViewModels;
using ShoppingCart.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Application.Services
{
    public class CartsService : ICartsService
    {
        private ICartsRepository _cartRepo;

        public CartsService(ICartsRepository cartRepository)
        {
            _cartRepo = cartRepository;
        }

        public IQueryable<CartViewModel> getcarts()
        {
            var list = from c in _cartRepo.GetProducts()
                       select new CartViewModel()
                       {
                          
                       };
            return list;
        }

        public ProductViewModel GetProduct(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
