using MediatR;

namespace WebCatalog.Logic.CQRS.Reviews.Commands.UpdateReview;

public class UpdateReviewCommand : IRequest
{
    public int ReviewId { get; set; }
    public string? Content { get; set; }
    public int Rating { get; set; }
}