using Microsoft.AspNetCore.Mvc;
using DEV1_2024_Assignment.ViewModels;
using DEV1_2024_Assignment.Services;
using DEV1_2024_Assignment.Models;
using Microsoft.AspNetCore.Identity;

namespace DEV1_2024_Assignment.Controllers;

public class ProductsController : Controller
{
    private readonly ProductService _productService;
    private readonly UserManager<AppUser> _userManager;
    
    public ProductsController(ProductService productService, UserManager<AppUser> userManager)
    {
        _productService = productService;
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        // Pass ApplicationDbContext to the IndexViewModel
        var model = new IndexViewModel();
        model.Products = _productService.GetProducts();
        return View(model);
    }
    [HttpGet]
    public  IActionResult Cart()
    {
        // Pass ApplicationDbContext to the CartViewModel
        var model = new CartViewModel();
        if(User.Identity.IsAuthenticated)
        {
            var userId = _userManager.GetUserId(User);
            model.Customer = _userManager.FindByIdAsync(userId).Result;
            model.Customer.Cart = new List<Product>();
            model.Customer.Cart.Add(new Product{Name = "pippo", Price=55});
            model.Customer.Cart.Add(new Product{Name = "pip", Price=15});
            model.Customer.Cart.Add(new Product{Name = "pi", Price=15});
            model.Customer.Cart.Add(new Product{Name = "peeip", Price=15});
            model.Customer.Cart.Add(new Product{Name = "p", Price=15});
            

        }

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
            _productService.AddProduct(product);
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
    public IActionResult Index(decimal? maxPrice, decimal? minPrice, string? brandName, string? name, int? pageIndex = 1)
    {
        var model = new IndexViewModel();
        model.MinPrice = minPrice;
        model.MaxPrice = maxPrice;
        model.Products = _productService.GetProducts();
        model.Products = _productService.FilterProducts(model.Products, brandName, name, maxPrice, minPrice);
        //model.Products  = model.Products.OrderBy(p => p.Name).ToList(); -------->>>>> Da implementare nel service!!!!!!!!!!!!
        model.PageNumber = (int)Math.Ceiling(model.Products.Count / 6.0);
        model.Products = model.Products.Skip(((pageIndex ?? 1) - 1) * 6).Take(6).ToList();
        
        return View(model);
    }
    
}