using MediatR;

namespace WebCatalog.Logic.WebCatalog.Reviews.Commands.CreateReview;

public class CreateReviewCommand : IRequest<int>
{
    public int ProductId { get; set; }
    public int Rating { get; set; }
    public string? Content { get; set; }
}