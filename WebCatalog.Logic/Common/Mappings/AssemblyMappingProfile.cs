using System.Reflection;
using AutoMapper;

namespace WebCatalog.Logic.Common.Mappings;

public class AssemblyMappingProfile : Profile
{
    public AssemblyMappingProfile()
    {
        ApplyMappingFromAssembly(Assembly.GetExecutingAssembly());
    }

    private void ApplyMappingFromAssembly(Assembly assembly)
    {
        var types = assembly.GetExportedTypes()
            .Where(type => type.GetInterfaces()
                .Any(i =>
                    i.IsGenericType &&
                    i.GetGenericTypeDefinition() == typeof(IMapWith<>)))
            .ToList();

        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);
            var methodName = nameof(IMapWith<int>.Mapping);
            var interfaceName = typeof(IMapWith<>).Name;
            var methodInfo = type.GetMethod(methodName)
                             ?? type.GetInterface(interfaceName)
                                 .GetMethod(methodName);

            methodInfo?.Invoke(instance, new object[] {this});
        }
    }
}