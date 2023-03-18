using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebCatalog.Infrastructure.DataBase;
using WebCatalog.Infrastructure.Services;
using WebCatalog.Logic.Common.ExternalServices;

namespace WebCatalog.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        var useInMemoryDatabase = true;

        if (configuration["UseInMemoryDatabase"] != null)
        {
            useInMemoryDatabase = bool.Parse(configuration["UseInMemoryDatabase"]);
        }

        if (useInMemoryDatabase)
        {
            services.AddDbContext<ApplicationDbContext>(optionsAction =>
                optionsAction.UseInMemoryDatabase("WebCatalogInMemoryDb"));
        }
        else
        {
            services.AddDbContext<ApplicationDbContext>(optionsAction =>
                optionsAction.UseSqlServer(configuration.GetConnectionString("MsSql")));
        }

        services.AddScoped<AppDbContext>(provider => provider.GetService<ApplicationDbContext>());

        services.AddTransient<IDateTimeService, DateTimeService>();

        return services;
    }
}