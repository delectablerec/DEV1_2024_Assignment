using Microsoft.AspNetCore.Mvc;
using DEV1_2024_Assignment.Data;
using DEV1_2024_Assignment.Models;
using Microsoft.EntityFrameworkCore;


namespace DEV1_2024_Assignment.Controllers;

    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Name")] Product product)
        {
            if (!ModelState.IsValid) return View(product);

            _context.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }

