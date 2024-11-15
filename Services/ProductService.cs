
using DEV1_2024_Assignment.Models;
using DEV1_2024_Assignment.Data;
using Newtonsoft.Json;

namespace DEV1_2024_Assignment.Services;

    public class ProductService
    {
        private const string SAVEPATH = "wwwroot//cartFiles";
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
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
        public void UpdateCart(string id, Product product){
            string path = Path.Combine(SAVEPATH,id + ".json");
            if(!File.Exists(path))
                File.Create(path).Close();
            using (StreamWriter sw = new StreamWriter(path)){                                          
                sw.Write(JsonConvert.SerializeObject(product, Formatting.Indented));  
            }
        }
        public List<Product> FilterProducts(List<Product> productsToFilter, string? brandName, string? name, decimal? minPrice, decimal? maxPrice){
            List<Product> filtredProducts = new List<Product>();
            bool addToList;
            if(productsToFilter != null){

                foreach(Product prod in productsToFilter){
                    addToList = true;

                    if(!string.IsNullOrEmpty(name) && prod.Name != name)
                        addToList = false;

                    if(!string.IsNullOrEmpty(brandName) && prod.Brand.UserName != brandName)
                        addToList = false;

                    if(minPrice.HasValue && prod.Price < minPrice)
                        addToList = false;
                    
                    if(maxPrice.HasValue && prod.Price > maxPrice)
                        addToList = false;
                    
                    if(addToList)
                        filtredProducts.Add(prod);
                }
            }
            return filtredProducts;
        }
    
    }
