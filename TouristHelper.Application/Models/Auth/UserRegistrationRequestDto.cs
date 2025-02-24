namespace TouristHelper.Application.Models.Auth;

public class UserRegistrationRequestDto
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
