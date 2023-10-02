using MediatR;

namespace WebCatalog.Logic.WebCatalog.Brands.Commands.UpdateBrand;

public class UpdateBrandCommand : IRequest
{
    public int BrandId { get; set; }

    public string Name { get; set; } = default!;
}