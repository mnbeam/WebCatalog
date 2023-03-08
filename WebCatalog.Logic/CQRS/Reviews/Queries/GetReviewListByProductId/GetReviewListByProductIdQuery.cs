using MediatR;

namespace WebCatalog.Logic.CQRS.Reviews.Queries.GetReviewListByProductId;

public class GetReviewListByProductIdQuery : IRequest<GetReviewListByProductIdVm>
{
    public int ProductId { get; set; }
}