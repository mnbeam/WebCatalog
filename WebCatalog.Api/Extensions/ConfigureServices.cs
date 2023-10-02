using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WebCatalog.Domain.Enums;
using WebCatalog.Logic.Common.Configurations;
using WebCatalog.Logic.Common.Exceptions;
using WebCatalog.Logic.Common.Extensions;

namespace WebCatalog.Api.Extensions;

/// <summary>
/// Методы расширения для API системы.
/// </summary>
public static class ConfigureServices
{
    /// <summary>
    /// Добавить аутентификацию и авторизацию.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <param name="configuration">Конфигурация.</param>
    /// <returns>Коллекция сервисов.</returns>
    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var authOptions = configuration
                              .GetSection(nameof(AuthOptions))
                              .Get<AuthOptions>()
                          ?? throw new ArgumentException(
                              $"Configuration {nameof(AuthOptions)} not found.");

        services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = authOptions.Issuer,

                    ValidateAudience = true,
                    ValidAudience = authOptions.Audience,

                    ValidateLifetime = true,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = authOptions.SymmetricSecurityKey
                };
            });

        services.AddAuthorization(option =>
        {
            option.AddPolicy("ForAdmin", policy =>
                policy.RequireAssertion(x =>
                    x.User.HasClaim(ClaimTypes.Role, Role.Admin.GetEnumDescription())));

            option.AddPolicy("ForSeller", policy =>
                policy.RequireAssertion(x =>
                    x.User.HasClaim(ClaimTypes.Role, Role.Seller.GetEnumDescription()) ||
                    x.User.HasClaim(ClaimTypes.Role, Role.Admin.GetEnumDescription())));
        });
        
        return services;
    }
    
    /// <summary>
    /// Добавить Swagger.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <returns>Коллекция сервисов.</returns>
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(opts =>
        {
            opts.SwaggerDoc("v1", new OpenApiInfo { Title = "WebCatalog" });
            opts.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"
            });

            opts.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        return services;
    }
}