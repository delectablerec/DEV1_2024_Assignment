using Microsoft.AspNetCore.Mvc;
using DEV1_2024_Assignment.Models;
using DEV1_2024_Assignment.Services;
using DEV1_2024_Assignment.Data;
using DEV1_2024_Assignment.ViewModels;

namespace DEV1_2024_Assignment.Controllers;
public class HomeController : Controller
{
    private readonly ProductService _productService;
    private readonly ApplicationDbContext _context;

    public HomeController(ProductService productService, ApplicationDbContext context)
    {
        _productService = productService;
        _context = context;
    }

    public IActionResult Index()
    {
        var randomProducts = _productService.GetProducts()
                                            .OrderBy(x => Guid.NewGuid())
                                            .Take(5)
                                            .ToList();

        var model = new HomeViewModel
        {
            RandomProducts = randomProducts,
            TotalProducts = _context.GetProducts().Count(),
            // TotalPurchases = _context.Purchases.Count(),
            TotalCustomers = _context.Users.Count(),
            // TotalBrands = _context.Brands.Count()
        };

        return View(model);
    }
}