using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebCatalog.Logic.Common.ExternalServices;

namespace WebCatalog.Logic.WebCatalog.Brands.Queries.GetBrandList;

public class GetBrandListQueryHandler : IRequestHandler<GetBrandListQuery, BrandListVm>
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetBrandListQueryHandler(AppDbContext dbContext,
        IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<BrandListVm> Handle(GetBrandListQuery request,
        CancellationToken cancellationToken)
    {
        return new BrandListVm
        {
            Brands = await _dbContext.Brands
                .ProjectTo<BrandVm>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken)
        };
    }
}