using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DEV1_2024_Assignment.Data;
using DEV1_2024_Assignment.Models;
using DEV1_2024_Assignment.Services;
using Microsoft.AspNetCore.Http.Features;

namespace DEV1_2024_Assignment;

public class Program()
{
	static async Task Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

//************************************************************************************************************************
		builder.Services.Configure<FormOptions>(options =>
		{
			options.MultipartBodyLengthLimit = 10 * 1024 * 1024; // Limita il file upload a 10 MB
		});
//************************************************************************************************************************
		
		// Add services to the container.
		var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
		builder.Services.AddDbContext<ApplicationDbContext>(options =>
			options.UseSqlite(connectionString));
		builder.Services.AddDatabaseDeveloperPageExceptionFilter();

		builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
			.AddRoles<IdentityRole>()
			.AddEntityFrameworkStores<ApplicationDbContext>();
		builder.Services.AddControllersWithViews();
		builder.Services.AddScoped<ProductService>();


		var app = builder.Build();

		// Configure the HTTP request pipeline.
		if (app.Environment.IsDevelopment())
		{
			app.UseMigrationsEndPoint();
		}
		else
		{
			app.UseExceptionHandler("/Home/Error");
			// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
			app.UseHsts();
		}

		app.UseHttpsRedirection();
		app.UseStaticFiles();

		app.UseRouting();
		app.UseAuthorization();

		app.UseEndpoints(endpoints =>
		{
			endpoints.MapControllerRoute(
				name: "user",
				pattern: "User/{email}",
				defaults: new { controller = "Users", action = "Index" });
		});

		app.MapControllerRoute(
			name: "default",
			pattern: "{controller=Home}/{action=Index}/{id?}");
		app.MapRazorPages();

		// Seeding del database
		using (var scope = app.Services.CreateScope())
		{
			var serviceProvider = scope.ServiceProvider;
			try
			{
				var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
				var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
				await SeedData.InitializeAsync(userManager, roleManager);
			}
			catch (Exception ex)
			{
				var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
				logger.LogError(ex, "Un errore è avvenuto durante il seeding del database.");
			}
		}
		app.Run();
	}

}
