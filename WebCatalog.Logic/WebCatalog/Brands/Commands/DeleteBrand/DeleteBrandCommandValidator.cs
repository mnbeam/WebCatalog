using FluentValidation;
using WebCatalog.Logic.Common.Extensions;

namespace WebCatalog.Logic.WebCatalog.Brands.Commands.DeleteBrand;

public class DeleteBrandCommandValidator : AbstractValidator<DeleteBrandCommand>
{
    public DeleteBrandCommandValidator()
    {
        RuleFor(v => v.BrandId).MustBePositive();
    }
}