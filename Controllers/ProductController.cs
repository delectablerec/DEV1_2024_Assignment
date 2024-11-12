using Microsoft.AspNetCore.Mvc;
using DEV1_2024_Assignment.ViewModels;
using DEV1_2024_Assignment.Services;

namespace DEV1_2024_Assignment.Controllers;

public class ProductsController : Controller
{
    private ServiceProducts _service;
    public ProductsController(ServiceProducts service){
        _service = service;
    }
    [HttpGet]
    public IActionResult Details(int id)
    {
        var model = new DetailsViewModel();       
        model.Product = _service.GetProductById(id);
        return View(model);
    }
}

