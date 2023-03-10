using MediatR;

namespace WebCatalog.Logic.WebCatalog.Reviews.Commands.DeleteReview;

public class DeleteReviewCommand : IRequest
{
    public int ReviewId { get; set; }
}