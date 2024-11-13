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
}
