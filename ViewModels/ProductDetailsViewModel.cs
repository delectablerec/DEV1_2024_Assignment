using DEV1_2024_Assignment.Models;
using DEV1_2024_Assignment.Services;

namespace DEV1_2024_Assignment.ViewModels;

public class DetailsViewModel
{
    public Product Product { get; set; }
    private ServiceProducts _service;

    public DetailsViewModel(int id)
    {
        _service = new ServiceProducts();
        Product = _service.GetProductById(id);
    }
}
