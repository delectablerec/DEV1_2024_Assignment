
using DEV1_2024_Assignment.Models;
using DEV1_2024_Assignment.Data;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;

namespace DEV1_2024_Assignment.Services;

public class ProductService
{
    private const string SAVEPATH = "wwwroot//cartFiles";
    private const string IMAGEPATH = "wwwroot//logos";

    private readonly ApplicationDbContext _context;
    private readonly UserManager<AppUser> _userManager;

    public ProductService(ApplicationDbContext context, UserManager<AppUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public List<Product> GetProducts()
    {
        return _context.GetProducts();
    }
    public int GetPurchases()
    {
        return _context.GetPurchases().Count;
    }
    public List<Product> GetIndexProducts()
    {
        var products = new List<Product>();
        foreach (var p in _context.GetProducts())
        {
            if (p.IsApproved)
                products.Add(p);
        }
        return products;
    }

    public Dictionary<string, string> GetBrands(List<AppUser> users) //l'ho messo nel service dei prodotti per ora
    {
        var tempDictionary = new Dictionary<string, string>();
        foreach (var u in users)
        {
            if (u.IsBrand)
            {
                if(u.Logo == null)
                    u.Logo = "";
                tempDictionary.Add(u.UserName,"/logos/"+u.Logo);
            }
        }

        return tempDictionary;
    }

    public int GetCustomers()
    {
        int tot = 0;
        foreach (var u in _userManager.Users.ToList())
        {
            if (!u.IsBrand)
                tot++;
        }
        return tot;
    }

    public void ApproveProduct(int id)
    {
        foreach (var p in _context.GetProducts())
        {
            if (p.Id == id)
            {
                p.IsApproved = true;
                break;
            }
        }
        _context.SaveChanges();
    }

    public void DeleteProduct(int id)
    {
        foreach (var p in _context.GetProducts())
        {
            if (p.Id == id)
            {
                _context.RemoveProduct(p);
                break;
            }
        }
        _context.SaveChanges();
    }

    // Method to add a product to the database
    public void AddProduct(Product product)
    {
        _context.Add(product);
        _context.SaveChanges(); // Save changes to the database
    }
    public void EditProduct(Product product)
    {
        _context.EditProduct(product);
    }
    public Product GetProductById(int id)
    {
        Product foundProduct = null;
        foreach (var product in _context.GetProducts())
        {
            if (product.Id == id)
            {
                foundProduct = product;
                break;
            }
        }
        return foundProduct;
    }
    public void LoadProductsTable()
    {
        foreach (var p in GetProducts())
        {
            foreach (var c in _userManager.Users.ToList())
            {
                if (p.BrandId == c.Id)
                {
                    p.Brand = c;
                    break;
                }
            }
        }
    }
    public void SaveChanges()
    {
        _context.SaveChanges();
    }
    public void UpdateCart(string userId, List<Product> products)
    {
        string path = Path.Combine(SAVEPATH, userId + ".json");
        if (!File.Exists(path))
            File.Create(path).Close();
        using (StreamWriter sw = new StreamWriter(path))
        {
            sw.Write(JsonConvert.SerializeObject(products, Formatting.Indented));
        }
    }
    public int Purchase(Product product, string userId)
    {
        int result = _context.CheckProductStock(product);
        if (result >= 0)
        {
            _context.Purchase(product.Id, userId);
            return result;
        }
        return result;
    }
    public List<Product> ReadCart(string userId)
    {
        string path = Path.Combine(SAVEPATH, userId + ".json");

        // Se il file non esiste, restituisci una lista vuota
        if (!File.Exists(path))
        {
            return new List<Product>();
        }

        // Leggi il contenuto del file
        string json = File.ReadAllText(path);

        // Deserializza il contenuto JSON in una lista di oggetti Product
        List<Product> products = JsonConvert.DeserializeObject<List<Product>>(json);

        // Restituisci la lista di prodotti
        return products ?? new List<Product>();
    }
    public List<Product> FilterProducts(List<Product> productsToFilter, string? brandName, string? name, decimal? minPrice, decimal? maxPrice)
    {
        List<Product> filteredProducts = new List<Product>();
        bool addToList;

        if (productsToFilter != null)
        {
            foreach (Product prod in productsToFilter)
            {
                addToList = true;
                if (!string.IsNullOrEmpty(brandName) && brandName != prod.Brand.UserName)
                    addToList = false;

                if (!string.IsNullOrEmpty(name) && name != prod.Name)
                    addToList = false;

                if (minPrice.HasValue && prod.Price < minPrice)
                    addToList = false;

                if (maxPrice.HasValue && prod.Price > maxPrice)
                    addToList = false;

                if (addToList)
                    filteredProducts.Add(prod);
            }
        }
        return filteredProducts;
    }
    public List<Product> GetProductsByBrand(string id)
    {
        var brandProducts = new List<Product>();
        foreach (var p in _context.GetProducts())
        {
            if (p.BrandId == id)
            {
                brandProducts.Add(p);
            }
        }
        return brandProducts;
    }

    public decimal CalculateTotalPrice(List<Product> cart)
    {
        decimal total = 0;
        if (cart != null)
        {
            foreach (var product in cart)
            {
                total += product.Price * product.Stock; // Multiply price by stock quantity
            }
        }
        return total;
    }


    public List<Product> GetProductsToApprove()
    {
        var productsToApprove = new List<Product>();
        foreach (var p in _context.GetProducts())
        {
            if (!p.IsApproved)
                productsToApprove.Add(p);

        }
        return productsToApprove;
    }



}
