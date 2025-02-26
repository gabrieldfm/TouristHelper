using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TouristHelper.Application.Interfaces;
using TouristHelper.Application.Models.Auth;
using TouristHelper.Domain.Entities;
using TouristHelper.Domain.Interfaces;

namespace TouristHelper.Application.Services;

public class AuthService(IUserRepository userRepository, IConfiguration configuration) : IAuthService
{
    public async Task<UserRegistrationResponseDto?> RegisterAsync(UserRegistrationRequestDto request)
    {
        if (await userRepository.GetByEmailAsync(request.Email) is not null)
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

    public async Task<TokenResponseDto?> LoginAsync(UserLoginRequestDto request)
    {
        var user = await userRepository.GetByEmailAsync(request.Email);
        if (user is null || new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password) == PasswordVerificationResult.Failed)
        {
            return null;
        }

        return await CreateTokenResponse(user);
    }

    public async Task<TokenResponseDto?> RefreshTokensAsync(RefreshTokenRequestDto request)
    {
        var user = await ValidateRefreshTokenAsync(request.UserId, request.RefreshToken);
        if (user is null)
        {
            return null;
        }

        return await CreateTokenResponse(user);
    }

    private async Task<User?> ValidateRefreshTokenAsync(Guid userId, string resfreshToken)
    {
        var user = await userRepository.GetByIdAsync(userId);
        if (user is null || user.RefreshToken != resfreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            return null;
        }

        return user;
    }

    private async Task<TokenResponseDto> CreateTokenResponse(User user)
    {
        return new TokenResponseDto
        {
            AccessToken = CreateToken(user),
            RefreshToken = await GenerateAndSaveRefreshTokenAsync(user)
        };
    }

    private async Task<string> GenerateAndSaveRefreshTokenAsync(User user)
    {
        var refreshToken = GenerateRefreshToken();
        await userRepository.UpdateRefreshTokenAsyn(user, refreshToken);

        return refreshToken;
    }

    private string CreateToken(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Role, user.Role)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(configuration["Appsettings:token"]!));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        var tokenDescript = new JwtSecurityToken(
            issuer: configuration["Appsettings:Issuer"],
            audience: configuration["Appsettings:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(tokenDescript);
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }    
}
