using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebCatalog.Logic.Configurations;

namespace WebCatalog.Logic.Extensions;

public static class AppSettingsHelperExtension
{
    public static IServiceCollection AddAppSettingsHelper(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<AuthOptions>(
            configuration.GetSection("AuthOptions"));

        return services;
    }
}