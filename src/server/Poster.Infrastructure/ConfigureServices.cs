using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Poster.Domain;

namespace Poster.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options => 
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        
        services.AddScoped<IApplicationDbContext>(provider => 
            provider.GetService<ApplicationDbContext>()!);
        
        services.AddIdentity<User, IdentityRole>(config => 
            { 
                config.Password.RequiredLength = 4; 
                config.Password.RequireDigit = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
                config.Password.RequireLowercase = false;
                config.SignIn.RequireConfirmedEmail = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        
        return services;
    }
}