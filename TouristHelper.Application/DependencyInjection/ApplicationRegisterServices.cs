using Microsoft.Extensions.DependencyInjection;
using TouristHelper.Application.Interfaces;

namespace TouristHelper.Application.DependencyInjection;

public static class ApplicationRegisterServices
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, IAuthService>();

        return services;
    }
}
