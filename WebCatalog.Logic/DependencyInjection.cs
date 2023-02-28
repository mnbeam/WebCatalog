using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebCatalog.Logic.Extensions;
using WebCatalog.Logic.Services.Accounts;
using WebCatalog.Logic.Services.Tokens;
using WebCatalog.Logic.Validators;

namespace WebCatalog.Logic;

public static class DependencyInjection
{
    public static IServiceCollection AddLogic(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAppSettingsHelper(configuration);

        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddSingleton<AuthValidator>();

        return services;
    }
}