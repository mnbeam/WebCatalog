using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebCatalog.Logic.Common.ExternalServices;

namespace WebCatalog.Logic.WebCatalog.Reviews.Queries.GetReviewListByProductId;

public class GetQueryListByProductIdQueryHandler : IRequestHandler<GetReviewListByProductIdQuery,
    ReviewListVm>
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetQueryListByProductIdQueryHandler(AppDbContext dbContext,
        IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ReviewListVm> Handle(GetReviewListByProductIdQuery request,
        CancellationToken cancellationToken)
    {
        return new ReviewListVm
        {
            ReviewVms = await _dbContext.Reviews
                .Where(r => r.ProductId == request.ProductId)
                .ProjectTo<ReviewVm>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken)
        };
    }
}