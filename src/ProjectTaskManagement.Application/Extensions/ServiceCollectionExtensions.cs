using Microsoft.Extensions.DependencyInjection;
using ProjectTaskManagement.Application.Services;

namespace ProjectTaskManagement.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<AuthService>();
        services.AddScoped<ProjectService>();
        services.AddScoped<TaskService>();

        return services;
    }
}
