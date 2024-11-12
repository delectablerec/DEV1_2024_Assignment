using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DEV1_2024_Assignment.Models
{
    public class Brand
    {
        public int Id { get; set; }
        public string AppUserId { get; set; }
        public AppUser appUser { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}