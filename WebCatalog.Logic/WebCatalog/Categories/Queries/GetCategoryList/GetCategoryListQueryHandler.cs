using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebCatalog.Logic.Common.ExternalServices;
using WebCatalog.Logic.WebCatalog.Categories.Queries.GetCategory;

namespace WebCatalog.Logic.WebCatalog.Categories.Queries.GetCategoryList;

public class GetCategoryListQueryHandler : IRequestHandler<GetCategoryListQuery, CategoryListVm>
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetCategoryListQueryHandler(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<CategoryListVm> Handle(GetCategoryListQuery request,
        CancellationToken cancellationToken)
    {
        var categories = await _dbContext.Categories
            .ProjectTo<GetCategoryVm>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new CategoryListVm
        {
            Categories = categories
        };
    }
}