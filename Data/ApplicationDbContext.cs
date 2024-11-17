using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DEV1_2024_Assignment.Models;

namespace DEV1_2024_Assignment.Data;

public class ApplicationDbContext : IdentityDbContext<AppUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    private DbSet<Brand> _brands { get; set; }
    private DbSet<Product> _products { get; set; }
    private DbSet<Purchase> _purchases { get; set; }

    public List<Product> GetProducts(){
        return _products.ToList();
    }
    public void AddProduct(){
        
    }
    public int CheckProductStock(Product product)
    {
        foreach (var p in _products.ToList())
        {
            if (p.Id == product.Id){
                if(p.Stock >= product.Stock){
                    p.Stock -= product.Stock;
                    SaveChanges();
                    return p.Stock;
                }else{
                    return -1;
                }
            }
        }
        return -1;
    }
    // Method to add a purchase to the database
    public void Purchase(int productId, string userId)
    {
        // Retrieve the product from the database
        var product = _products.FirstOrDefault(p => p.Id == productId);
        if (product == null)
        {
            throw new Exception("Product not found.");
        }

        // Check if the user exists
        var user = Users.FirstOrDefault(u => u.Id == userId);
        if (user == null)
        {
            throw new Exception("User not found.");
        }

        // Create a new Purchase object
        var newPurchase = new Purchase
        {
            ProductId = product.Id,
            Product = product,
            AppUserId = user.Id,
            AppUser = user,
            PurchaseDate = DateTime.Now
        };

        // Add the new purchase to the database
        _purchases.Add(newPurchase);

        // Save changes to the database
        SaveChanges();
    }
}
