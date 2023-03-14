using MediatR;
using Microsoft.EntityFrameworkCore;
using WebCatalog.Domain.Entities.ProductEntities;
using WebCatalog.Logic.Common.Configurations;
using WebCatalog.Logic.Common.Exceptions;
using WebCatalog.Logic.Common.ExternalServices;
using WebCatalog.Logic.WebCatalog.Baskets.Commands.CreateBasket;

namespace WebCatalog.Logic.WebCatalog.Baskets.Commands.RemoveBasketItem;

public class RemoveBasketItemCommandHandler : IRequestHandler<RemoveBasketItemCommand>
{
    private readonly AppDbContext _dbContext;
    private readonly IMediator _mediator;
    private readonly IUserAccessor _userAccessor;

    public RemoveBasketItemCommandHandler(AppDbContext dbContext,
        IUserAccessor userAccessor,
        IMediator mediator)
    {
        _dbContext = dbContext;
        _userAccessor = userAccessor;
        _mediator = mediator;
    }

    public async Task Handle(RemoveBasketItemCommand request, CancellationToken cancellationToken)
    {
        var basket = await _dbContext.Baskets
            .Where(b => b.UserId == _userAccessor.UserId)
            .FirstOrDefaultAsync(cancellationToken);

        if (basket == null)
        {
            var createBasketCommand = new CreateBasketCommand
            {
                UserId = _userAccessor.UserId
            };

            await _mediator.Send(createBasketCommand, cancellationToken);

            throw new WebCatalogEmptyBasketException();
        }

        var basketItem = await _dbContext.BasketItems
                             .Where(bi => bi.ProductId == request.ProductId &&
                                          bi.BasketId == basket.Id)
                             .FirstOrDefaultAsync(cancellationToken)
                         ?? throw new WebCatalogNotFoundException(nameof(Product),
                             request.ProductId);

        if (basketItem.Quantity > request.Quantity)
        {
            basketItem.Quantity -= request.Quantity;
        }
        else
        {
            _dbContext.BasketItems.Remove(basketItem);
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}