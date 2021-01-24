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
    public class OrdersService : IOrdersService
    {

        private IOrdersRepository _orderRep;
        private IMapper _mapper;

        public OrdersService(IOrdersRepository OrderRepository, IMapper mapper)
        {
            _mapper = mapper;
            _orderRep = OrderRepository;
        }
        public void Checkout(string email)
        {
            Order newOrder = new Order()
            {
                DatePlaced = DateTime.Now,
                UserEmail = email
            };
            _orderRep.AddOrder(newOrder);
        }

        public IQueryable<OrderViewModel> GetOrder()
        {
            var carts = _orderRep.GetOrder().ProjectTo<OrderViewModel>(_mapper.ConfigurationProvider);

            return carts;
        }

    }
}
