using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PresentationWebApp.Models;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.ViewModels;
using ShoppingCart.Domain.Models;

namespace PresentationWebApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsService _productsService;
        private readonly ICategoriesService _categoriesService;
        private IWebHostEnvironment _env;
        public ProductsController(IProductsService productsService, ICategoriesService categoriesService,
             IWebHostEnvironment env )
        {
            _productsService = productsService;
            _categoriesService = categoriesService;
            _env = env;
        }

        public async Task<IActionResult> Index(int pageNumber=1)
        {
            var list = _productsService.GetProducts();
            var listOfCategeories = _categoriesService.GetCategories();
            ViewBag.Categories = listOfCategeories;
            return View(await PagingList<ProductViewModel>.CreateAsync(list, pageNumber,5));

        }



        [HttpPost]
        public IActionResult Search(string keyword) //using a form, and the select list must have name attribute = category
        {
            var list = _productsService.GetProducts(keyword).ToList();
            var listOfCategeories = _categoriesService.GetCategories();
            ViewBag.Categories = listOfCategeories;
            return View("Index", list);
        }


        [HttpPost]
        public IActionResult Searchbycategory(int category) //using a form, and the select list must have name attribute = category
        {
            var list = _productsService.GetProducts(category).ToList();
            var listOfCategeories = _categoriesService.GetCategories();
            ViewBag.Categories = listOfCategeories;
            return View("Index", list);
        }


        public IActionResult Details(Guid id)
        {
            var p = _productsService.GetProduct(id);
            return View( p);
        }

        //the engine will load a page with empty fields
        [HttpGet]
        [Authorize (Roles ="Admin")] //is going to be accessed only by authenticated users
        public IActionResult Create()
        {
            //fetch a list of categories
            var listOfCategeories = _categoriesService.GetCategories();

            //we pass the categories to the page
            ViewBag.Categories = listOfCategeories;

            return View();
        }

        //here details input by the user will be received
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(ProductViewModel data, IFormFile f)
        {
            try
            {
                if(f !=  null)
                {
                    if(f.Length > 0)
                    {
                        //C:\Users\Ryan\source\repos\SWD62BEP\SWD62BEP\Solution3\PresentationWebApp\wwwroot
                        string newFilename = Guid.NewGuid() + System.IO.Path.GetExtension(f.FileName);
                        string newFilenameWithAbsolutePath = _env.WebRootPath +  @"\Images\" + newFilename;
                        
                        using (var stream = System.IO.File.Create(newFilenameWithAbsolutePath))
                        {
                            f.CopyTo(stream);
                        }

                        data.ImageUrl = @"\Images\" + newFilename;
                    }
                }

                _productsService.AddProduct(data);

                TempData["feedback"] = "Product was added successfully";
            }
            catch (Exception ex)
            {
                //log error
                TempData["warning"] = "Product was not added!";
            }

           var listOfCategeories = _categoriesService.GetCategories();
           ViewBag.Categories = listOfCategeories;
            return View(data);
        
        } //fiddler, burp, zap, postman

        [Authorize(Roles = "Admin")]
  
        public IActionResult Disable(Guid id)
        {
            try
            {
                _productsService.DisableProduct(id);
                TempData["feedback"] = "Product was deleted";
            }
            catch (Exception ex)
            {
                //log your error S
                TempData["warning"] = "Product was not deleted" + ex; //Change from ViewData to TempData
            }

            return RedirectToAction("Index");
        }




    }
}
