using DEV1_2024_Assignment.Models;

namespace DEV1_2024_Assignment.ViewModels;

    public class HomeViewModel
    {
        public List<Product> RandomProducts { get; set; }
        public int TotalProducts { get; set; }
        public int TotalPurchases { get; set; }
        public int TotalCustomers { get; set; }
        public int TotalBrands { get; set; }
    }

