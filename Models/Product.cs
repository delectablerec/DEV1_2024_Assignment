namespace DEV1_2024_Assignment.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string? BrandId { get; set; }
    public AppUser? Brand { get; set; }
    public string Details { get; set; }
    public bool IsApproved { get; set; }
}