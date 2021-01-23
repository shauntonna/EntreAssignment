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

        public CartController(IProductsService productsService, ICartsItemService CartItemService, ICartsService cartService)
        {
            _productsService = productsService;
            _cartItemsService = CartItemService;
            _cartService = cartService;

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
                        addnewItem.ItemId = cid;
                        addnewItem.ProductFk = product.id;
                        addnewItem.Quantity = 1;
                        _cartItemsService.Update(addnewItem);
                        _cartService.update(c);
                        return RedirectToAction("Index", "Products");
                    }

                }

                return RedirectToAction("Index", "Products");

                TempData["feedback"] = "Cart Item was added successfully";
            }
            catch (Exception ex)
            {
                //log error
                return RedirectToAction("error", "Home");
                TempData["warning"] = "Cart Item was not added!";
            }
        }

    }
}
