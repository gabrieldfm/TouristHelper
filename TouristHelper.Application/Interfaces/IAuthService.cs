using TouristHelper.Application.Models.Auth;

namespace TouristHelper.Application.Interfaces;

public interface IAuthService
{
    Task<UserRegistrationResponseDto?> RegisterAsync(UserRegistrationRequestDto request);
    Task<TokenResponseDto?> LoginAsync(UserLoginRequestDto request);
    Task<TokenResponseDto?> RefreshTokensAsync(RefreshTokenRequestDto request);
}
