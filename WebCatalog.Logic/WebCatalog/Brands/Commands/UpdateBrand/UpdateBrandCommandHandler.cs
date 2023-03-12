using MediatR;
using Microsoft.EntityFrameworkCore;
using WebCatalog.Domain.Entities.ProductEntities;
using WebCatalog.Logic.Common.Exceptions;
using WebCatalog.Logic.Common.ExternalServices;

namespace WebCatalog.Logic.WebCatalog.Brands.Commands.UpdateBrand;

public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand>
{
    private readonly AppDbContext _dbContext;

    public UpdateBrandCommandHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
    {
        var brand = await _dbContext.Brands
            .Where(b => b.Id == request.BrandId)
            .FirstOrDefaultAsync(cancellationToken);

        if (brand == null)
        {
            throw new WebCatalogNotFoundException(nameof(Brand), request.BrandId);
        }

        var isBrandDublicate = await _dbContext.Brands
            .Where(b => b.Name == request.Name)
            .AnyAsync(cancellationToken);

        if (isBrandDublicate)
        {
            throw new WebCatalogDublicateException(nameof(Brand), nameof(request.Name),
                request.Name);
        }

        brand.Name = request.Name;

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}