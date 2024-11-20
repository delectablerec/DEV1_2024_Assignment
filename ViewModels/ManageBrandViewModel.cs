using System.ComponentModel.DataAnnotations;

using DEV1_2024_Assignment.Models;

namespace DEV1_2024_Assignment.ViewModels;

public class ManageBrandViewModel
{
    public List<Product> Products { get; set; }
    public string? ProductName { get; set; }

    [Required(ErrorMessage = "Logo is mandatary")]
    public IFormFile Logo { get; set; }

}