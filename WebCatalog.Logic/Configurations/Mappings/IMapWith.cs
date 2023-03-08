using AutoMapper;

namespace WebCatalog.Logic.Configurations.Mappings;

public interface IMapWith<T>
{
    void Mapping(Profile profile) =>
        profile.CreateMap(typeof(T), GetType());
}