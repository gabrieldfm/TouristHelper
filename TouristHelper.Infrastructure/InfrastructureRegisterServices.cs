using Microsoft.Extensions.DependencyInjection;
using TouristHelper.Domain.Interfaces;
using TouristHelper.Infrastructure.Repositories;

namespace TouristHelper.Infrastructure;

public static class InfrastructureRegisterServices
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
