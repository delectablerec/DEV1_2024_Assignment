using Microsoft.AspNetCore.Identity;

namespace DEV1_2024_Assignment.Models;
public class AppUser : IdentityUser
{
    public string? Surname {get;set;}    
    public string? Address {get;set;}
    public string? Logo {get;set;}
    public bool IsBrand{get;set;} = false;
}