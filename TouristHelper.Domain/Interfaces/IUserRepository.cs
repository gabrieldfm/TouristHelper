using TouristHelper.Domain.Entities;

namespace TouristHelper.Domain.Interfaces;

public interface IUserRepository
{
    Task<User> CreateUserAsync(User user);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByIdAsync(Guid id);
    Task UpdateRefreshTokenAsyn(User user, string refreshToken);
}
