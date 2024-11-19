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

    //l'ho messo nel service dei prodotti per ora
    public List<Brand> GetBrands(){
        return _brands.ToList();
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
    public bool Purchase(int productId, string userId)
    {
        bool stillExist = false;
        // Retrieve the product from the database
        foreach(var p in _products.ToList()){
            if(productId==p.Id) 
                stillExist = true;
        }
        if(!stillExist)
        {
            return false;
        }
        // Check if the user exists


        // Create a new Purchase object
        var newPurchase = new Purchase
        {
            ProductId = productId,
            AppUserId = userId,
            PurchaseDate = DateTime.Now
        };

        // Add the new purchase to the database
        _purchases.Add(newPurchase);

        // Save changes to the database
        SaveChanges();
        return true;
    }
}
