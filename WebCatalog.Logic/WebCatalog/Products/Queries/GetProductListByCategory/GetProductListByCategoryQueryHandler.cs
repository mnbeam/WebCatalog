using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebCatalog.Logic.Common.ExternalServices;
using WebCatalog.Logic.WebCatalog.Products.Queries.GetProduct;
using WebCatalog.Logic.WebCatalog.Products.Queries.GetProductList;

namespace WebCatalog.Logic.WebCatalog.Products.Queries.GetProductListByCategory;

public class GetProductListByCategoryQueryHandler : IRequestHandler<GetProductListByCategoryQuery,
    ProductListVm>
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetProductListByCategoryQueryHandler(AppDbContext dbContext,
        IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ProductListVm> Handle(GetProductListByCategoryQuery request,
        CancellationToken cancellationToken)
    {
        return new ProductListVm
        {
            Products = await _dbContext.Products
                .Where(p => p.CategoryId == request.CategoryId)
                .ProjectTo<ProductVm>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken)
        };
    }
}