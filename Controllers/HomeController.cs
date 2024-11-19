using Microsoft.AspNetCore.Mvc;
using DEV1_2024_Assignment.Services;
using DEV1_2024_Assignment.ViewModels;

namespace DEV1_2024_Assignment.Controllers;
public class HomeController : Controller
{
    private readonly ProductService _productService;

    public HomeController(ProductService productService)
    {
        _productService = productService;
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
            TotalProducts = _productService.GetProducts().Count(),
            // TotalPurchases = _context.Purchases.Count(),
            // TotalCustomers = _context.Users.Count(),
            // TotalBrands = _context.Brands.Count()
        };

        return View(model);
    }
}