using MediatR;
using WebCatalog.Domain.Entities.ProductEntities;
using WebCatalog.Logic.Common.Exceptions;
using WebCatalog.Logic.Common.ExternalServices;

namespace WebCatalog.Logic.WebCatalog.Products.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand>
{
    private readonly AppDbContext _dbContext;

    public CreateProductCommandHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(CreateProductCommand request,
        CancellationToken cancellationToken)
    {
        var isCategoryExist = _dbContext.Categories.Any(c => c.Id == request.CategoryId);

        if (!isCategoryExist)
        {
            throw new WebCatalogNotFoundException(nameof(Category), request.CategoryId);
        }

        var isBrandExist = _dbContext.Brands.Any(b => b.Id == request.BrandId);

        if (!isBrandExist)
        {
            throw new WebCatalogNotFoundException(nameof(Brand), request.BrandId);
        }

        var product = new Product
        {
            CategoryId = request.CategoryId,
            BrandId = request.BrandId,
            Name = request.Name,
            Description = request.Description,
            Price = request.Price
        };

        await _dbContext.Products.AddAsync(product, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}