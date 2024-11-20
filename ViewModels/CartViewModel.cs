using DEV1_2024_Assignment.Models;
using DEV1_2024_Assignment.ViewModels;
namespace DEV1_2024_Assignment.ViewModels
{
    public class CartViewModel
    {
        public List<Product> Cart { get; set; }

        // Computed property to get the total price of all products in the cart
        public decimal TotalPrice { get; set; }
    }
}
