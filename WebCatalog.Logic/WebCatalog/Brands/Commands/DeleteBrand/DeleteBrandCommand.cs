using MediatR;

namespace WebCatalog.Logic.WebCatalog.Brands.Commands.DeleteBrand;

public class DeleteBrandCommand : IRequest
{
    public int BrandId { get; set; }
}