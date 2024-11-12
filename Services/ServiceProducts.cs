using DEV1_2024_Assignment.Models;
using DEV1_2024_Assignment.Data;

namespace DEV1_2024_Assignment.Services;


public class ServiceProducts
{
    private ApplicationDbContext _context;

    public ServiceProducts(ApplicationDbContext context)
    {
        _context = context;
    }
    public List<Product> GetProducts()
    {
        return _context.GetProducts();
    }
    public void AddProduct()
    {
    }
    public Product GetProductById(int id)
    {
        Product foundProduct = null;
        // Loop through each product to find the one with the matching ID
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
}
