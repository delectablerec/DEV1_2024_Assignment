using Microsoft.AspNetCore.Mvc;
using DEV1_2024_Assignment.ViewModels;
using DEV1_2024_Assignment.Services;
using DEV1_2024_Assignment.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

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
    public IActionResult Cart()
    {
        var model = new CartViewModel();

        if (User.Identity.IsAuthenticated)
        {
            var userId = _userManager.GetUserId(User);
            model.Customer = _userManager.FindByIdAsync(userId).Result;

            // Read the cart from the JSON file using ProductService
            model.Customer.Cart = _productService.ReadCart(userId);

            // If the cart is still null or empty, initialize it
            if (model.Customer.Cart == null || model.Customer.Cart.Count == 0)
            {
                model.Customer.Cart = new List<Product>();
            }
        }
        else
        {
            // If the user is not authenticated, the cart is empty
            model.Customer = new AppUser { Cart = new List<Product>() };
        }

        return View(model);
    }

    [HttpPost]
    public IActionResult AddToCart(int productId)
    {
        if (User.Identity.IsAuthenticated)
        {
            var userId = _userManager.GetUserId(User);
            var product = _productService.GetProductById(productId);
            List<Product> tempCart = _productService.ReadCart(userId);
            if (tempCart.Count == 0)
                tempCart.Add(product);
            else
            {
                bool add = true;
                foreach (Product p in tempCart)
                {
                    if (p.Id == product.Id)
                        add = false;
                }
                if (add)
                    tempCart.Add(product);
            }
            _productService.UpdateCart(userId, tempCart);

            /*
            // Create a cart object for JSON serialization
            var cartDetails = new
            {
                UserId = user.Id,
                UserName = user.UserName,
                ProductId = product.Id,
                ProductName = product.Name,
                ProductPrice = product.Price
            };

            var json = JsonConvert.SerializeObject(cartDetails);
            var directoryPath = Path.Combine("wwwroot", "cart_files");

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var filePath = Path.Combine(directoryPath, $"{user.UserName}_{user.Id}.json");

             Save JSON file
            System.IO.File.WriteAllText(filePath, json); */

            return RedirectToAction("Index");

        }

        // If not authenticated or product not found, redirect to Index
        return RedirectToAction("Index");
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