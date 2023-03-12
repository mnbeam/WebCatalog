using MediatR;
using Microsoft.EntityFrameworkCore;
using WebCatalog.Domain.Entities.BasketEntities;
using WebCatalog.Logic.Common.Configurations;
using WebCatalog.Logic.Common.ExternalServices;
using WebCatalog.Logic.WebCatalog.Baskets.Commands.CreateBasket;

namespace WebCatalog.Logic.WebCatalog.Baskets.Commands.AddBasketItem;

public class AddBasketItemCommandHandler : IRequestHandler<AddBasketItemCommand>
{
    private readonly AppDbContext _dbContext;
    private readonly IMediator _mediator;
    private readonly IUserAccessor _userAccessor;

    public AddBasketItemCommandHandler(AppDbContext dbContext,
        IUserAccessor userAccessor,
        IMediator mediator)
    {
        _dbContext = dbContext;
        _userAccessor = userAccessor;
        _mediator = mediator;
    }

    public async Task Handle(AddBasketItemCommand request, CancellationToken cancellationToken)
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

            basket = await _mediator.Send(createBasketCommand, cancellationToken);
        }

        var basketItem = await _dbContext.BasketItems
            .Where(bi => bi.ProductId == request.ProductId &&
                         bi.BasketId == basket.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (basketItem == null)
        {
            var newBasketItem = new BasketItem
            {
                BasketId = basket.Id,
                ProductId = request.ProductId,
                Quantity = request.Quantity
            };

            await _dbContext.AddAsync(newBasketItem, cancellationToken);
        }
        else
        {
            basketItem.Quantity += request.Quantity;
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}