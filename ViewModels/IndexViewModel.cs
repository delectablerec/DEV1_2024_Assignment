using DEV1_2024_Assignment.Models;
using DEV1_2024_Assignment.Services;

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

    public IndexViewModel()
    {
        _service = new ServiceProducts();
        Products = _service.GetProducts();
    }
}
