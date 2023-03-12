using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebCatalog.Logic.Common.ExternalServices;
using WebCatalog.Logic.WebCatalog.Products.Queries.GetProduct;

namespace WebCatalog.Logic.WebCatalog.Products.Queries.GetProductList;

public class GetProductListQueryHandler : IRequestHandler<GetProductListQuery, ProductListVm>
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetProductListQueryHandler(AppDbContext dbContext,
        IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ProductListVm> Handle(GetProductListQuery request,
        CancellationToken cancellationToken)
    {
        return new ProductListVm
        {
            Products = await _dbContext.Products
                .ProjectTo<ProductVm>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken)
        };
    }
}