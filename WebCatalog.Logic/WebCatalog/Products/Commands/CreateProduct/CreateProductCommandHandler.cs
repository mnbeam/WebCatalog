using MediatR;
using Microsoft.EntityFrameworkCore;
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
        await CheckCategoryAndBrandExistingAndThrowAsync(request, cancellationToken);

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

    private async Task CheckCategoryAndBrandExistingAndThrowAsync(CreateProductCommand request,
        CancellationToken cancellationToken)
    {
        var isCategoryExist =
            await _dbContext.Categories.AnyAsync(c => c.Id == request.CategoryId, cancellationToken);

        if (!isCategoryExist)
        {
            throw new WebCatalogNotFoundException(nameof(Category), request.CategoryId);
        }

        var isBrandExist = await _dbContext.Brands.AnyAsync(b => b.Id == request.BrandId,
            cancellationToken: cancellationToken);

        if (!isBrandExist)
        {
            throw new WebCatalogNotFoundException(nameof(Brand), request.BrandId);
        }
    }
}