using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebCatalog.Domain.Entities.ProductEntities;
using WebCatalog.Logic.Common.Exceptions;
using WebCatalog.Logic.Common.ExternalServices;

namespace WebCatalog.Logic.WebCatalog.Products.Queries.GetProduct;

public class GetProductQueryHandler : IRequestHandler<GetProductQuery, ProductVm>
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetProductQueryHandler(AppDbContext dbContext,
        IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ProductVm> Handle(GetProductQuery request,
        CancellationToken cancellationToken)
    {
        var product = await _dbContext.Products
            .Where(p => p.Id == request.ProductId)
            .FirstOrDefaultAsync(cancellationToken);

        if (product == null)
        {
            throw new NotFoundException(nameof(Product), request.ProductId);
        }

        return _mapper.Map<ProductVm>(product);
    }
}