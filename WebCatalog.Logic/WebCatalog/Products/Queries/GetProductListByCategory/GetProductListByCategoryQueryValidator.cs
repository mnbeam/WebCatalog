using FluentValidation;
using WebCatalog.Logic.Common.Extensions;

namespace WebCatalog.Logic.WebCatalog.Products.Queries.GetProductListByCategory;

public class
    GetProductListByCategoryQueryValidator : AbstractValidator<GetProductListByCategoryQuery>
{
    public GetProductListByCategoryQueryValidator()
    {
        RuleFor(v => v.CategoryId).ValidateId();
    }
}