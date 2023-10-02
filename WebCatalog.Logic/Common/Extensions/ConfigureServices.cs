using System.Reflection;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebCatalog.Logic.Common.Behaviors;
using WebCatalog.Logic.Common.Mappings;

namespace WebCatalog.Logic.Common.Extensions;

public static class ConfigureServices
{
    public static IServiceCollection AddLogic(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAppSettingsHelper(configuration);

        services.AddMediatR(c => c.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services
            .AddValidatorsFromAssemblies(new[] {Assembly.GetExecutingAssembly()});
        services.AddTransient(typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>));

        var mapperConfiguration =
            new MapperConfiguration(mc => mc.AddProfile(new AssemblyMappingProfile()));
        mapperConfiguration.AssertConfigurationIsValid();
        services.AddSingleton(mapperConfiguration.CreateMapper());

        return services;
    }
}