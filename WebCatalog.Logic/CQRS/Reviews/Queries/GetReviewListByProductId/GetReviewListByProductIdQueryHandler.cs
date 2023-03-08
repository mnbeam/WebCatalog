using MediatR;

namespace WebCatalog.Logic.CQRS.Reviews.Queries.GetReviewListByProductId;

public class GetQueryListByProductIdQueryHandler : IRequestHandler<GetReviewListByProductIdQuery,
    GetReviewListByProductIdVm>
{
    public async Task<GetReviewListByProductIdVm> Handle(GetReviewListByProductIdQuery request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}