using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristHelper.Application.Models.Auth;
using TouristHelper.Domain.Entities;

namespace TouristHelper.Application.Interfaces;

public interface IAuthService
{
    Task<UserRegistrationResponseDto?> RegisterAsync(UserRegistrationRequestDto request);
}
