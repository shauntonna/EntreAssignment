using AutoMapper;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.ViewModels;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Application.Services
{
    public class CartsService : ICartsService
    {
        private ICartsRepository _cartRep;
        private IMapper _mapper;
    
 
       
        public CartsService(ICartsRepository cartRepository, IMapper mapper)
        {
            _mapper = mapper;
            _cartRep = cartRepository;
        }

        public void addcart(CartViewModel cart)
        {

            Cart newCart = new Cart()
            {
                Id = cart.id,
                price = cart.Price,
                Email = cart.Email
            };

            _cartRep.AddProduct(newCart);

        }

        public void deletecart(CartViewModel cart)
        {
            var pToDelete = _cartRep.userCart(cart.Email);

            if (pToDelete != null)
            {
                _cartRep.delete(pToDelete);
            }
        }

        public CartViewModel GetCart(string email)
        {
            var userCart = _cartRep.userCart(email);
            var result = _mapper.Map<Cart, CartViewModel>(userCart);
            return result;
        }

        public void update(CartViewModel cart)
        {
            Cart Update = _cartRep.userCart(cart.Email);
            double t = 0;
            foreach(var i in Update.CartItems)
            {
                t += i.Product.Price + i.Qty;
            }
            Update.price = t;

            _cartRep.update(Update);

            var result = _mapper.Map<Cart, CartViewModel>(Update);
        }
    }
}
