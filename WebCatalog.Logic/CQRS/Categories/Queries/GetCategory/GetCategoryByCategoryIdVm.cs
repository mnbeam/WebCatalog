using AutoMapper;
using WebCatalog.Domain.Entities.ProductEntities;
using WebCatalog.Logic.Configurations.Mappings;

namespace WebCatalog.Logic.CQRS.Categories.Queries.GetCategory;

public class GetCategoryVm : IMapWith<Category>
{
    public string? Name { get; set; }
    public string? Description { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Category, GetCategoryVm>()
            .ForMember(categoryVm => categoryVm.Name,
                opt => opt.MapFrom(category => category.Name))
            .ForMember(categoryVm => categoryVm.Description,
            opt => opt.MapFrom(category => category.Description));
    }
}