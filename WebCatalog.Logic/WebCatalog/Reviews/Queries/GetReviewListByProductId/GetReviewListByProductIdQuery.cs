using MediatR;

namespace WebCatalog.Logic.WebCatalog.Reviews.Queries.GetReviewListByProductId;

public class GetReviewListByProductIdQuery : IRequest<ReviewListVm>
{
    public int ProductId { get; set; }
}