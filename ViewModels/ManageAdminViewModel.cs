using DEV1_2024_Assignment.Models;

namespace DEV1_2024_Assignment.ViewModels
{
    public class ManageAdminViewModel
    {
        public List<Product> ProductsToApprove { get; set; }
        public int PageNumber { get; set; }
        public string? ProductName { get; set; }
    }
}