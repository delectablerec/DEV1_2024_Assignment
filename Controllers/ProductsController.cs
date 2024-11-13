using Microsoft.AspNetCore.Mvc;
using DEV1_2024_Assignment.ViewModels;
using DEV1_2024_Assignment.Data;
using DEV1_2024_Assignment.Services;
using DEV1_2024_Assignment.Models;

namespace DEV1_2024_Assignment.Controllers;

    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ServiceProducts _service;

        public ProductsController(ApplicationDbContext context, ServiceProducts service)
        {
            _context = context;
            _service = service;
        }

        public IActionResult Index()
        {
            // Pass ApplicationDbContext to the IndexViewModel
            var model = new IndexViewModel(_context);
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

