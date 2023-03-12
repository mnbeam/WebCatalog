using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebCatalog.Logic.Common.ExternalServices;
using WebCatalog.Logic.WebCatalog.Reviews.Queries.GetReviewListByProductId;

namespace WebCatalog.Logic.WebCatalog.Reviews.Queries.GetReviewListByUserId;

public class GetReviewListByUserIdQueryHandler : IRequestHandler<GetReviewListByUserIdQuery,
    ReviewListVm>
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetReviewListByUserIdQueryHandler(AppDbContext dbContext,
        IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ReviewListVm> Handle(GetReviewListByUserIdQuery request,
        CancellationToken cancellationToken)
    {
        return new ReviewListVm
        {
            Reviews = await _dbContext.Reviews
                .Where(r => r.UserId == request.UserId)
                .ProjectTo<ReviewVm>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken)
        };
    }
}