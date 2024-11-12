using DEV1_2024_Assignment.Models;
using DEV1_2024_Assignment.Data;

namespace DEV1_2024_Assignment.Services
{
    public class ServiceProducts
    {
        private ApplicationDbContext _database;
        public List<Product> GetProducts(){
            return _database.GetProducts();
        }
        public Product GetProductById(int id){
            Product foundProduct = null;
            // Loop through each product to find the one with the matching ID
            foreach (var product in _database.GetProducts())
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
}