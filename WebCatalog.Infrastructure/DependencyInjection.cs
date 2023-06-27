using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebCatalog.Domain.Entities;
using WebCatalog.Infrastructure.DataBase;
using WebCatalog.Infrastructure.Services;
using WebCatalog.Infrastructure.Services.Logger;
using WebCatalog.Logic.Common.ExternalServices;
using WebCatalog.Logic.Common.ExternalServices.Email;

namespace WebCatalog.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration, string rootPath)
    {
        services.AddIdentity<AppUser, IdentityRole<int>>(options =>
            {
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
        
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
                optionsAction.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        }

        services.AddScoped<AppDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddTransient<IDateTimeService, DateTimeService>();
        services.AddSingleton(new FileLogger(rootPath));
        services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

        services.Configure<EmailConfiguration>(
            configuration.GetSection(EmailConfiguration.SectionName));
        services.AddScoped<IEmailService, EmailService>();

        return services;
    }

    public static async Task SeedDatabaseAsync(this IServiceProvider serviceProvider, ILogger logger)
    {
        using var scope = serviceProvider.CreateScope();
        var scopedProvider = scope.ServiceProvider;
        try
        {
            var dbContext = scopedProvider.GetRequiredService<AppDbContext>();
            var userManager = scopedProvider.GetRequiredService<UserManager<AppUser>>();
            var roleManager = scopedProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();

            await ApplicationDbContextSeed.SeedAsync(dbContext, userManager, roleManager, logger);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred seeding the DB:");
        }
    }
}