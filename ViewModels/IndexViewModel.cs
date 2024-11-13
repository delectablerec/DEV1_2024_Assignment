using DEV1_2024_Assignment.Models;
using DEV1_2024_Assignment.Services;
using DEV1_2024_Assignment.Data;
using System.Collections.Generic;

namespace DEV1_2024_Assignment.ViewModels;

    public class IndexViewModel
    {
        public List<Product> Products { get; set; }
        public int PageNumber { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string? Brand { get; set; }
        public string? ProductName { get; set; }
        private ServiceProducts _service;

        // Constructor accepting ApplicationDbContext
        public IndexViewModel(ApplicationDbContext context)
        {
            _service = new ServiceProducts(context);
            Products = _service.GetProducts();
        }
    }

