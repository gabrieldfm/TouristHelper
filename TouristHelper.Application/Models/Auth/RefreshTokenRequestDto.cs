﻿namespace TouristHelper.Application.Models.Auth;

public class RefreshTokenRequestDto
{
    public Guid UserId { get; set; }
    public required string RefreshToken { get; set; }
}
