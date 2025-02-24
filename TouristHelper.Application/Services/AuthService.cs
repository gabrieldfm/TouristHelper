using Microsoft.AspNetCore.Identity;
using TouristHelper.Application.Interfaces;
using TouristHelper.Application.Models.Auth;
using TouristHelper.Domain.Entities;
using TouristHelper.Domain.Interfaces;

namespace TouristHelper.Application.Services;

public class AuthService(IUserRepository userRepository) : IAuthService
{
    public async Task<UserRegistrationResponseDto?> RegisterAsync(UserRegistrationRequestDto request)
    {
        if (await userRepository.GetByEmailAsync(request.Email) is null)
        {
            return null;
        }

        var user = new User();

        var hashedPassword = new PasswordHasher<User>()
           .HashPassword(user, request.Password);

        user.Email = request.Email;
        user.PasswordHash = hashedPassword;

        var userCreated = await userRepository.CreateUserAsync(user);

        return new UserRegistrationResponseDto { Email = userCreated.Email};
    }
}
