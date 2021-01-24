using AutoMapper;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Application.Services
{
    public class OrderDetailsService : IOrderDetailsService
    {
        private ICartItemRepository _cartItemRep;
        private IOrderDetailsRepository _orderDetRep;
        private IOrdersRepository _orderRep;
        private IMapper _mapper;

        public OrderDetailsService(IOrderDetailsRepository OrderDetailRep, IMapper mapper, ICartItemRepository cartItem, IOrdersRepository Order)
        {
            _mapper = mapper;
            _orderDetRep = OrderDetailRep;
            _cartItemRep = cartItem;
            _orderRep = Order;
        }

        
        public void addOrderDetails(string email)
        {
            
                var Order = _orderRep.GetOrder().OrderByDescending(x => x.DatePlaced).Where(x => x.UserEmail == email).FirstOrDefault();
                var cart = _cartItemRep.GetCartItems().Where(x => x.Cart.Email == email).FirstOrDefault();
                foreach (var x in cart.Cart.CartItems)
                {
                    OrderDetails newDetails = new OrderDetails
                    {

                        Order = Order,
                        Product = cart.Product,
                        ProductFK = cart.Productid,
                        OrderFK = Order.Id,
                        Quantity = cart.Qty,
                        Price = cart.Product.Price


                    };

                    _orderDetRep.AddOrderDetail(newDetails);
                }
            
            
        }
    }
}
