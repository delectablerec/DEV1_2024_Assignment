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

        var userId = _userManager.GetUserId(User);

        // Read the cart from the JSON file using ProductService
        model.Cart  = _productService.ReadCart(userId);

        return View(model);
    }

    [HttpPost]
    public IActionResult UpdateCartQuantity(int productId, int quantity)
    {
        Console.WriteLine("ciao   ciao");
        if (User.Identity.IsAuthenticated)
        {
            var userId = _userManager.GetUserId(User);
            var product = _productService.GetProductById(productId);
            List<Product> tempCart = _productService.ReadCart(userId);
            
            foreach (Product p in tempCart)
            {
                if (p.Id == product.Id)
                {
                    p.Stock = quantity;
                    break;
                }
            }
            
            _productService.UpdateCart(userId, tempCart);

            return RedirectToAction("Cart");
        }
        return RedirectToAction("Cart");
    }
    [HttpPost]
    public IActionResult RemoveFromCart(int productId)
    {
        if (User.Identity.IsAuthenticated)
        {
            var userId = _userManager.GetUserId(User);
            var product = _productService.GetProductById(productId);
            List<Product> tempCart = _productService.ReadCart(userId);
            
            foreach (Product p in tempCart)
            {
                if (p.Id == product.Id)
                {
                    tempCart.Remove(p);
                    break;
                }
            }
            
            _productService.UpdateCart(userId, tempCart);

            return RedirectToAction("Cart");
        }
        return RedirectToAction("Cart");
    }
    [HttpGet]
    public  IActionResult CompletedPurchase()
    {
        var model = new CompletedPurchaseViewModel();
        var userId = _userManager.GetUserId(User);
        var user =  _userManager.FindByIdAsync(userId).Result;
        model.Name = user.UserName;
        return View(model);
    }
    [HttpPost]
    public IActionResult AddToCart(int productId)
    {
        if (User.Identity.IsAuthenticated)
        {
            var userId = _userManager.GetUserId(User);
            var product = _productService.GetProductById(productId);
            product.Stock = 1;
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

            return RedirectToAction("Index");
        }
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
        model.Products = _productService.FilterProducts(model.Products, brandName, name, minPrice, maxPrice);
        //model.Products  = model.Products.OrderBy(p => p.Name).ToList(); -------->>>>> Da implementare nel service!!!!!!!!!!!!
        model.PageNumber = (int)Math.Ceiling(model.Products.Count / 6.0);
        model.Products = model.Products.Skip(((pageIndex ?? 1) - 1) * 6).Take(6).ToList();

        return View(model);
    }
    [HttpGet]
public IActionResult Details(int productId)
{
    var product = _productService.GetProductById(productId);

    if (product == null)
    {
        // If the product is not found, redirect to Index or show an error page
        return RedirectToAction("Index");
    }

    // Create a DetailsViewModel and populate it with the product details
    var model = new DetailsViewModel
    {
        Id = product.Id,
        Name = product.Name,
        Price = product.Price,
        Stock = product.Stock,
        Details = product.Details,
        BrandName = product.Brand?.UserName
    };

    return View(model); // Return the Details view with the model
}
[HttpPost]
public IActionResult SubmitProductFromBrandPage(Product product)
{
    if (User.Identity.IsAuthenticated)
    {
        // Set the product as not approved
        product.IsApproved = false;
        product.BrandId = _userManager.GetUserId(User);

        // Save the product using ProductService
        _productService.AddProduct(product);

        return RedirectToAction("Index"); // Redirect to the brand page or wherever needed
    }
    return RedirectToAction("Index");
}


}