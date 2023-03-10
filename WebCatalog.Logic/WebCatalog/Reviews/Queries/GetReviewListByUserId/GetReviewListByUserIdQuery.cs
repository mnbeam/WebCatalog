using MediatR;
using WebCatalog.Logic.WebCatalog.Reviews.Queries.GetReviewListByProductId;

namespace WebCatalog.Logic.WebCatalog.Reviews.Queries.GetReviewListByUserId;

public class GetReviewListByUserIdQuery : IRequest<ReviewListVm>
{
    public int UserId { get; set; }
}