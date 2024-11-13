using DEV1_2024_Assignment.Models;

namespace DEV1_2024_Assignment.ViewModels;

    public class IndexViewModel
    {
        public List<Product> Products { get; set; }
        public int PageNumber { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string? Brand { get; set; }
        public string? ProductName { get; set; }
    }

