using FluentValidation;
using WebCatalog.Logic.Common.Extensions;

namespace WebCatalog.Logic.WebCatalog.Reviews.Commands.DeleteReview;

public class DeleteReviewCommandValidator : AbstractValidator<DeleteReviewCommand>
{
    public DeleteReviewCommandValidator()
    {
        RuleFor(v => v.ReviewId).MustBePositive();
    }
}