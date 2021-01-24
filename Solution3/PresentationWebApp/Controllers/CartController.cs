using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.ViewModels;

namespace PresentationWebApp.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductsService _productsService;
        private readonly ICartsItemService _cartItemsService;
        private readonly ICartsService _cartService;
        private readonly IOrdersService _OrderService;

        public CartController(IProductsService productsService, IOrdersService Order , ICartsItemService CartItemService, ICartsService cartService)
        {
            _productsService = productsService;
            _cartItemsService = CartItemService;
            _cartService = cartService;
            _OrderService = Order;
        }

        public IActionResult Index()
        {
            var cart = GetCart();
            return View(cart);
        }

        public CartViewModel GetCart()
        {
            var email = User.Identity.Name;
            var c = _cartService.GetCart(email);

            if (c == null)
            {
                CartViewModel newcart = new CartViewModel()
                {
                    Email = email,
                    Price = 0
                };
                _cartService.addcart(newcart);
                c = _cartService.GetCart(email);
            }
            return c;
        }
        public IActionResult AddCart(Guid id)
        {
            try
            {
                var c = GetCart();
                int cid = c.id;

                var product = _productsService.GetProduct(id);
                if (product != null)
                {
                    var IE = _cartItemsService.CheckToSee(cid, id);
                        if (IE != null)
                        {
                        _cartItemsService.Update(IE);
                        _cartService.update(c);
                         return RedirectToAction("Index", "Products");
                        }
                    else
                    {
                        CartItemViewModel addnewItem = new CartItemViewModel();
                        addnewItem.CartIdFK = cid;
                        addnewItem.ProductFk = product.id;
                        addnewItem.Quantity = 1;
                        _cartItemsService.addCart(addnewItem);
                        _cartService.update(c);
                        return RedirectToAction("Index", "Products");
                    }

                }
                TempData["feedback"] = "Cart Item was added successfully";
                return RedirectToAction("Index", "Products");

               
            }
            catch (Exception ex)
            {
                //log error
                TempData["warning"] = "Cart Item was not added!";
                return RedirectToAction("error", "Home");
                
            }
        }

        public IActionResult Delete()
        {
            
                try
                {
                var c = GetCart();
                   if(c != null)
                    {
                    _cartService.deletecart(c);
                        TempData["feedback"] = "Cart was deleted";
                    }
                    
                }
                catch (Exception ex)
                {
                    //log your error S
                    TempData["warning"] = "Product was not deleted" + ex; //Change from ViewData to TempData
                    return RedirectToAction("error", "Home");
                }

                return RedirectToAction("Index");
         
        }

        public IActionResult AddOrder()
        {

            try
            {
                var c = GetCart();
                _OrderService.Checkout(c.Email);
                _cartService.deletecart(c);
                TempData["feedback"] = "Order was added";
            }
            catch (Exception ex)
            {
                //log your error S
                TempData["warning"] = "Order was not deleted" + ex; //Change from ViewData to TempData
                return RedirectToAction("error", "Home");
            }

            return RedirectToAction("Index");

        }

        public IActionResult AddOrderDetails()
        {

            try
            {
                var c = GetCart();
                _OrderService.Checkout(c.Email);
                TempData["feedback"] = "Order was added";
            }
            catch (Exception ex)
            {
                //log your error S
                TempData["warning"] = "Order was not deleted" + ex; //Change from ViewData to TempData
                return RedirectToAction("error", "Home");
            }

            return RedirectToAction("Index");

        }

    }
}
