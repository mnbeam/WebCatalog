using FluentValidation;
using WebCatalog.Logic.Common.Extensions;

namespace WebCatalog.Logic.WebCatalog.Reviews.Queries.GetReviewListByProductId;

public class GetReviewListByProductIdQueryValidator :
    AbstractValidator<GetReviewListByProductIdQuery>
{
    public GetReviewListByProductIdQueryValidator()
    {
        RuleFor(v => v.ProductId).MustBePositive();
    }
}