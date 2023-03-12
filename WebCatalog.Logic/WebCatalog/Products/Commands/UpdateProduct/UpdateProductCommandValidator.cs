using FluentValidation;
using WebCatalog.Logic.Common.Extensions;

namespace WebCatalog.Logic.WebCatalog.Products.Commands.UpdateProduct;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(v => v.ProductId).MustBePositive();

        RuleFor(v => v.CategoryId).MustBePositive();

        RuleFor(v => v.CategoryId).MustBePositive();

        RuleFor(v => v.Name).ValidateName();

        RuleFor(v => v.Price).NotEmpty();
    }
}