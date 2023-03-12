using FluentValidation;
using WebCatalog.Logic.Common.Extensions;

namespace WebCatalog.Logic.WebCatalog.Baskets.Commands.AddBasketItem;

public class AddBasketItemCommandValidator : AbstractValidator<AddBasketItemCommand>
{
    public AddBasketItemCommandValidator()
    {
        RuleFor(v => v.ProductId).MustBePositive();

        RuleFor(v => v.Quantity)
            .Must(q => q >= 1).WithMessage("Invalid quantity.");
    }
}