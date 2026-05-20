using Microsoft.Extensions.DependencyInjection;
using MediatR;
using ProjectTaskManagement.Application.Services;

namespace ProjectTaskManagement.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(typeof(ServiceCollectionExtensions).Assembly);
        services.AddScoped<AuthService>();
        services.AddScoped<ProjectService>();
        services.AddScoped<TaskService>();

        return services;
    }
}
