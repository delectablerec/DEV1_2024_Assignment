using Microsoft.AspNetCore.Mvc;
using DEV1_2024_Assignment.ViewModels;
using DEV1_2024_Assignment.Data;
using DEV1_2024_Assignment.Services;
using DEV1_2024_Assignment.Models;

namespace DEV1_2024_Assignment.Controllers;

    public class ProductsController : Controller
    {
        private readonly ServiceProducts _service;

        public ProductsController(ServiceProducts service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            // Pass ApplicationDbContext to the IndexViewModel
            var model = new IndexViewModel();
            model.Products = _service.GetProducts();
            return View(model);
        }

        public IActionResult Cart()
        {
            // Pass ApplicationDbContext to the CartViewModel
            var model = new CartViewModel();
            model.Products = _service.GetProducts();
            return View(model);
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                _service.AddProduct(product);
                return RedirectToAction("Index"); // Redirect to the index page or wherever you'd like
            }
            return View(product);
        }
    }

