using DEV1_2024_Assignment.Models;
using DEV1_2024_Assignment.Data;
using System.Collections.Generic;

namespace DEV1_2024_Assignment.Services;

    public class ServiceProducts
    {
        private readonly ApplicationDbContext _context;

        public ServiceProducts(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Product> GetProducts()
        {
            return _context.GetProducts();
        }

        // Method to add a product to the database
        public void AddProduct(Product product)
        {
            _context.Add(product);
            _context.SaveChanges(); // Save changes to the database
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
    }

