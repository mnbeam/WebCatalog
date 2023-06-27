using MediatR;
using Microsoft.EntityFrameworkCore;
using WebCatalog.Domain.Entities.ProductEntities;
using WebCatalog.Logic.Common.Exceptions;
using WebCatalog.Logic.Common.ExternalServices;

namespace WebCatalog.Logic.WebCatalog.Brands.Commands.DeleteBrand;

public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand>
{
    private readonly AppDbContext _dbContext;

    public DeleteBrandCommandHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
    {
        var brand = await _dbContext.Brands
                        .FirstOrDefaultAsync(b => b.Id == request.BrandId, cancellationToken)
                    ?? throw new WebCatalogNotFoundException(nameof(Brand), request.BrandId);

        _dbContext.Brands.Remove(brand);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}