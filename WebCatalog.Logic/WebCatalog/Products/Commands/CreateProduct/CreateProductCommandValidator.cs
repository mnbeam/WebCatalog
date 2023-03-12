using FluentValidation;
using WebCatalog.Logic.Common.Extensions;

namespace WebCatalog.Logic.WebCatalog.Products.Commands.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(v => v.CategoryId).MustBePositive();

        RuleFor(v => v.BrandId).MustBePositive();

        RuleFor(v => v.Name).ValidateName();

        RuleFor(v => v.Price).NotEmpty();
    }
}