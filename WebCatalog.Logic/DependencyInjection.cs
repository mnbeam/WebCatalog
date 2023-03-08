using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebCatalog.Logic.Configurations.Mappings;
using WebCatalog.Logic.Extensions;
using WebCatalog.Logic.ExternalServices;

namespace WebCatalog.Logic;

public static class DependencyInjection
{
    public static IServiceCollection AddLogic(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAppSettingsHelper(configuration);
        services.AddMediatR(c => c.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddAutoMapper(config =>
        {
            config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
            config.AddProfile(new AssemblyMappingProfile(typeof(AppDbContext).Assembly));
        });

        return services;
    }
}