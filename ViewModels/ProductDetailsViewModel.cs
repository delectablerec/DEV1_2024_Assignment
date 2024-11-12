using DEV1_2024_Assignment.Migrations;
using DEV1_2024_Assignment.Models;
using DEV1_2024_Assignment.Services;
using DEV1_2024_Assignment.Data;
using Microsoft.EntityFrameworkCore;

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
