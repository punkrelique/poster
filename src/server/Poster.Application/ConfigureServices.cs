using Microsoft.Extensions.DependencyInjection;
using Poster.Application.Common.Interfaces;

namespace Poster.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IMessageService, MessageService>();

        return services;
    }
}