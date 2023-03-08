using MediatR;

namespace WebCatalog.Logic.CQRS.Reviews.Commands.DeleteReview;

public class DeleteReviewCommand : IRequest
{
    public int ReviewId { get; set; }
}