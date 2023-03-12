using FluentValidation;
using WebCatalog.Logic.Common.Extensions;

namespace WebCatalog.Logic.WebCatalog.Products.Queries.GetProductListByBrand;

public class GetProductListByBrandQueryValidator : AbstractValidator<GetProductListByBrandQuery>
{
    public GetProductListByBrandQueryValidator()
    {
        RuleFor(v => v.BrandId).MustBePositive();
    }
}