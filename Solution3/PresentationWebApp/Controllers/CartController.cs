using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Application.Interfaces;

namespace PresentationWebApp.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartsService _cartsService;
       
        private IWebHostEnvironment _env;

        public CartController(ICartsService cartsService, IWebHostEnvironment env)
        {
            _cartsService = cartsService;
            _env = env;
        }
        public IActionResult Index()
        {
            var list = _cartsService.getcarts();
            return View(list);
        }
        
    }
}
