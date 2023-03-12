using MediatR;
using Microsoft.EntityFrameworkCore;
using WebCatalog.Domain.Entities.ProductEntities;
using WebCatalog.Logic.Common.Exceptions;
using WebCatalog.Logic.Common.ExternalServices;

namespace WebCatalog.Logic.WebCatalog.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
{
    private readonly AppDbContext _dbContext;

    public UpdateProductCommandHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(UpdateProductCommand request,
        CancellationToken cancellationToken)
    {
        var product = await _dbContext.Products
            .FirstOrDefaultAsync(p => p.Id == request.ProductId,
                cancellationToken);

        if (product == null)
        {
            throw new WebCatalogNotFoundException(nameof(Product), request.ProductId);
        }

        product.CategoryId = request.CategoryId;
        product.BrandId = request.BrandId;
        product.Name = request.Name;
        product.Description = request.Description;
        product.Price = request.Price;

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}