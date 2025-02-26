using TouristHelper.Application.DependencyInjection;
using TouristHelper.Infrastructure.DependencyInjection;

namespace TouristHelper.API;

public static class RegisterServices
{
    public static IServiceCollection AddDependecies(this IServiceCollection services)
    {
        services.AddApplication();
        services.AddInfrastructure();
        return services;
    }
}
