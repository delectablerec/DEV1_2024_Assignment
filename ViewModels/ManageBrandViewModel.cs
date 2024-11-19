using DEV1_2024_Assignment.Models;

namespace DEV1_2024_Assignment.ViewModels
{
    public class ManageBrandViewModel
    {
        public List<Product> Products { get; set; }
        public Product Product { get; set; }
        public int PageNumber { get; set; }
        public string? ProductName { get; set; }
    }
}