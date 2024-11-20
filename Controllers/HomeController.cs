using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DEV1_2024_Assignment.Models;
using DEV1_2024_Assignment.Services;
using Microsoft.AspNetCore.Identity;
using DEV1_2024_Assignment.ViewModels;


namespace DEV1_2024_Assignment.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ProductService _productService;

    private readonly UserManager<AppUser> _userManager;

    public HomeController(ILogger<HomeController> logger, ProductService productService, UserManager<AppUser> userManager)
    {
        _logger = logger;
        _productService = productService;
        _userManager = userManager;
    }
    private void SetCartItemCountInViewBag()
    {
        if (User.Identity.IsAuthenticated)
        {
            var userId = _userManager.GetUserId(User);
            var cart = _productService.ReadCart(userId);
            ViewBag.TotalCartItems = cart.Count;
        }
        else
        {
            ViewBag.TotalCartItems = 0;
        }
    }
    [HttpGet]
    public IActionResult Index()
    {
        var model = new HomepageViewModel();
        model.Brands = _productService.GetBrands(_userManager.Users.ToList());
        model.TotalCustomers = _productService.GetCustomers();
        model.TotalProducts = _productService.GetProducts().Count;
        model.TotalPurchases = _productService.GetPurchases();
        SetCartItemCountInViewBag();

        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
