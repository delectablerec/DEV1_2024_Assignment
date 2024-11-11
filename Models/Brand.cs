using Microsoft.AspNetCore.Identity;

namespace DEV1_2024_Assignment.Models;

public class Brand : IdentityUser
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string Vat { get; set; }
    public string Logo { get; set; }
}