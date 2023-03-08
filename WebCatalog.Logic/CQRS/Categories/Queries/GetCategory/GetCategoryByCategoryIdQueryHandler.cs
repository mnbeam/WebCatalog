using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebCatalog.Domain.Entities.ProductEntities;
using WebCatalog.Logic.Exceptions;
using WebCatalog.Logic.ExternalServices;

namespace WebCatalog.Logic.CQRS.Categories.Queries.GetCategory;

public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, GetCategoryVm>
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetCategoryQueryHandler(IMapper mapper, AppDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<GetCategoryVm> Handle(GetCategoryQuery request,
        CancellationToken cancellationToken)
    {
        var category = await _dbContext.Categories
            .FirstOrDefaultAsync(c => c.Id == request.CategoryId,
                cancellationToken);

        if (category == null)
        {
            throw new NotFoundException(nameof(Category), request.CategoryId);
        }

        return _mapper.Map<GetCategoryVm>(category);
    }
}