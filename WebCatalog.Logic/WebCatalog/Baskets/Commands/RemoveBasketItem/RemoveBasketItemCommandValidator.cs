using FluentValidation;
using WebCatalog.Logic.Common.Extensions;

namespace WebCatalog.Logic.WebCatalog.Baskets.Commands.RemoveBasketItem;

public class RemoveBasketItemCommandValidator : AbstractValidator<RemoveBasketItemCommand>
{
    public RemoveBasketItemCommandValidator()
    {
        RuleFor(v => v.ProductId).MustBePositive();

        RuleFor(v => v.Quantity).MustBePositive();
    }
}