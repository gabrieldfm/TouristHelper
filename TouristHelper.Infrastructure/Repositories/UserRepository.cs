using Microsoft.EntityFrameworkCore;
using TouristHelper.Domain.Entities;
using TouristHelper.Domain.Interfaces;
using TouristHelper.Infrastructure.ApplicationContext;

namespace TouristHelper.Infrastructure.Repositories;

public class UserRepository(TouristHelperDbContext context) : IUserRepository
{
    public async Task<User> CreateUserAsync(User user)
    {
        context.Users.Add(user);
        await context.SaveChangesAsync();

        return user;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await context.Users.FindAsync(id);
    }

    public async Task UpdateRefreshTokenAsyn(User user, string refreshToken)
    {
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

        await context.SaveChangesAsync();
    }
}
