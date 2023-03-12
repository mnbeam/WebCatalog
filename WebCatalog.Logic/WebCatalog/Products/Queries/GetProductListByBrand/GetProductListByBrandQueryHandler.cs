using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebCatalog.Logic.Common.ExternalServices;
using WebCatalog.Logic.WebCatalog.Products.Queries.GetProduct;
using WebCatalog.Logic.WebCatalog.Products.Queries.GetProductList;

namespace WebCatalog.Logic.WebCatalog.Products.Queries.GetProductListByBrand;

public class
    GetProductListByBrandQueryHandler : IRequestHandler<GetProductListByBrandQuery, ProductListVm>
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetProductListByBrandQueryHandler(AppDbContext dbContext,
        IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ProductListVm> Handle(GetProductListByBrandQuery request,
        CancellationToken cancellationToken)
    {
        return new ProductListVm
        {
            Products = await _dbContext.Products
                .Where(p => p.BrandId == request.BrandId)
                .ProjectTo<ProductVm>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken)
        };
    }
}