using FluentValidation;
using WebCatalog.Logic.Common.Extensions;

namespace WebCatalog.Logic.WebCatalog.Reviews.Queries.GetReviewListByUserId;

public class GetReviewListByUserIdQueryValidator : AbstractValidator<GetReviewListByUserIdQuery>
{
    public GetReviewListByUserIdQueryValidator()
    {
        RuleFor(v => v.UserId).MustBePositive();
    }
}