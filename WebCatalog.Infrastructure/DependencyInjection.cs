using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebCatalog.Infrastructure.DataBase;
using WebCatalog.Logic.Abstractions;

namespace WebCatalog.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(optionsAction =>
            optionsAction.UseSqlServer(configuration.GetConnectionString("MsSql")));

        services.AddScoped<AppDbContext>(provider => provider.GetService<ApplicationDbContext>());
        
        return services;
    }
}