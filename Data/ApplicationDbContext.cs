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
}
