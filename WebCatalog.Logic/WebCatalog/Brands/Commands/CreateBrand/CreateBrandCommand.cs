using MediatR;

namespace WebCatalog.Logic.WebCatalog.Brands.Commands.CreateBrand;

public class CreateBrandCommand : IRequest
{
    public string Name { get; set; } = default!;
}