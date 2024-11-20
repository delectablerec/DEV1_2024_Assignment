using System.Security.Cryptography.X509Certificates;
using DEV1_2024_Assignment.Models;

namespace DEV1_2024_Assignment.ViewModels;

    public class HomepageViewModel
    {
        public Dictionary<string, string> Brands { get; set; }
        public int TotalProducts { get; set; }
        public int TotalCustomers { get; set; }
        public int TotalPurchases { get; set; }
    }
