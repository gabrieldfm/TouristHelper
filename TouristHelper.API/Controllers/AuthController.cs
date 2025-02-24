using Microsoft.AspNetCore.Mvc;
using TouristHelper.Application.Interfaces;
using TouristHelper.Application.Models.Auth;
using TouristHelper.Domain.Entities;

namespace TouristHelper.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService authService) : Controller
{
    [HttpPost("register")]
    public async Task<ActionResult<User>> Register(UserRegistrationRequestDto request)
    {
        var user = await authService.RegisterAsync(request);
        if (user is null)
        {
            return BadRequest("Email already exists");
        }

        return Ok(user);
    }
}
