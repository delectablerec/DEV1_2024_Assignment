using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DEV1_2024_Assignment.Models;
using Microsoft.AspNetCore.Identity;

namespace DEV1_2024_Assignment.Data;

public class ApplicationDbContext : IdentityDbContext<AppUser>
{
    private readonly UserManager<AppUser> _userManager;
    private DbSet<Product> _products { get; set; }
    private DbSet<Purchase> _purchases { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, UserManager<AppUser> userManager)
        : base(options)
    {
        _userManager = userManager;
    }
    public List<Product> GetProducts(){
        return _products.ToList();
    }

    //l'ho messo nel service dei prodotti per ora
    public Dictionary<string,string> GetBrands(){
        List<AppUser> users = _userManager.Users.ToList();
        var tempDictionary = new Dictionary<string,string>();
        foreach(var u in users)
        {
            if(u.IsBrand)
            {
                tempDictionary.Add(u.UserName, u.Logo);
            }
        }
        return tempDictionary;
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
