using AutoMapper;
using AutoMapper.QueryableExtensions;
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
    public class CartsItemService : ICartsItemService
    {
        
        private IMapper _mapper;
        private ICartItemRepository _cartItemRep;
        private ICartsRepository _cartsRep;
        private IProductsRepository _ProdRep;
        public CartsItemService(ICartItemRepository cartItemRe, ICartsRepository cartsRep, IMapper mapper, IProductsRepository productRepos)
        {
            _mapper = mapper;
            _cartItemRep = cartItemRe;
            _cartsRep = cartsRep;
            _ProdRep = productRepos;
        }

        //add cart item
        public void addCart(CartItemViewModel cartItem)
        {

            CartItem newCart = new CartItem()
            {
                Id = cartItem.ItemId,
                Qty = cartItem.Quantity,
                Productid = cartItem.ProductFk,
                Cartid = cartItem.CartIdFK,
           
                
            };

            _cartItemRep.addCart(newCart);
        }

        //delete cart item
        public void DeleteCart(CartItemViewModel cartItem)
        {
            var pToDelete = _cartItemRep.CartItem(cartItem.ItemId);

            if (pToDelete != null)
            {
                _cartItemRep.DeleteCart(pToDelete);
            }
        }

        public CartItemViewModel GetCartItem(int id)
        {
            var cartItems = _cartItemRep.CartItem(id);
            var result = _mapper.Map<CartItemViewModel>(cartItems);
            return result;
        }

        public IQueryable<CartItemViewModel> GetCartItems()
        {
            var carts = _cartItemRep.GetCartItems().ProjectTo<CartItemViewModel>(_mapper.ConfigurationProvider);

            return carts;
        }

        public void Update(CartItemViewModel cartItem)
        {
            var update = _cartItemRep.CartItem(cartItem.ItemId);

            if (update != null)
            {
                update.Qty = update.Qty + 1;

                _cartItemRep.Update(update);
            }
            
        }

        //returns the result of get cart items where the cart and product are equal to the parameters
        public CartItemViewModel CheckToSee(int id, Guid Product)
        {
            var cart = _cartItemRep.GetCartItems().Where(x => x.Cartid == id && x.Product.id == Product).FirstOrDefault();
            var result = _mapper.Map<CartItem, CartItemViewModel>(cart);
            CartItemViewModel newCartItem = new CartItemViewModel()
            {
                CartIdFK = cart.Cartid,
                ItemId = cart.Id,
                Product = result.Product,
                ProductFk = cart.Product.id,
                Quantity = cart.Qty
            };

            return newCartItem;
        }
       
        public CartViewModel userCart(string email)
        {
            var userCart = _cartsRep.userCart(email);
            var result = _mapper.Map<Cart, CartViewModel>(userCart);
            return result;
        }

    }
}
