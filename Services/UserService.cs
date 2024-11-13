using DEV1_2024_Assignment.Data;
using Microsoft.EntityFrameworkCore;
using DEV1_2024_Assignment.Models;

namespace DEV1_2024_Assignment.Services;
public class UserService
{
    private readonly ApplicationDbContext _context;

    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<AppUser>> GetAllUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }
}
    
