using Microsoft.AspNetCore.Identity;
using DEV1_2024_Assignment.Models;

namespace DEV1_2024_Assignment.Data
{
    public class SeedData
    {
        public static async Task InitializeAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            string[] roleNames = { "Admin", "Customer", "Brand" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                    await roleManager.CreateAsync(new IdentityRole(roleName));
            }

            if (await userManager.FindByEmailAsync("admin@admin.com") == null)
            {
                var adminUser = new AppUser
                {
                    Name = "Admin",
                    Surname = "AdminSur",
                    Address = "pippo",
                    UserName = "admin@admin.com",
                    Email = "admin@admin.com",
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(adminUser, "Password1@");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                    await userManager.AddToRoleAsync(adminUser, "Customer");
                    await userManager.AddToRoleAsync(adminUser, "Brand");
                }
            }
            else
            {
                var adminUser = await userManager.FindByEmailAsync("admin@admin.com");
                await userManager.AddToRoleAsync(adminUser, "Admin");
                await userManager.AddToRoleAsync(adminUser, "Customer");
                await userManager.AddToRoleAsync(adminUser, "Brand");
            }
        }
    }
}