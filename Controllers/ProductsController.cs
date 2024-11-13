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
            return RedirectToAction("Index");
        }
        else
        {
            // Log the validation errors for debugging
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }
        }
        return View(product);
    }

    [HttpGet]
    public IActionResult Index(decimal? maxPrice, decimal? minPrice, int? pageIndex = 1)
    {
        var model = new IndexViewModel();
        model.MinPrice = minPrice;
        model.MaxPrice = maxPrice;
        model.Products = _service.FilterProducts(model.Products, maxPrice, minPrice);
        model.Products = model.Products.OrderBy(p => p.Name).ToList();
        model.PageNumber = (int)Math.Ceiling(model.Products.Count / 6.0);
        model.Products = model.Products.Skip(((pageIndex ?? 1) - 1) * 6).Take(6).ToList();

        return View(model);
    }
}

