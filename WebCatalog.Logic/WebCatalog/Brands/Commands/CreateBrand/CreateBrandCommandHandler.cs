using MediatR;
using Microsoft.EntityFrameworkCore;
using WebCatalog.Domain.Entities.ProductEntities;
using WebCatalog.Logic.Common.Exceptions;
using WebCatalog.Logic.Common.ExternalServices;

namespace WebCatalog.Logic.WebCatalog.Brands.Commands.CreateBrand;

public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand>
{
    private readonly AppDbContext _dbContext;

    public CreateBrandCommandHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(CreateBrandCommand request, CancellationToken cancellationToken)
    {
        await CheckDublicateAndThrow(request);

        var brand = new Brand
        {
            Name = request.Name
        };

        await _dbContext.Brands.AddAsync(brand, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task CheckDublicateAndThrow(CreateBrandCommand request)
    {
        if (await _dbContext.Brands.AnyAsync(b => b.Name == request.Name))
        {
            throw new WebCatalogDublicateException(nameof(Brand), nameof(request.Name),
                request.Name);
        }
    }
}