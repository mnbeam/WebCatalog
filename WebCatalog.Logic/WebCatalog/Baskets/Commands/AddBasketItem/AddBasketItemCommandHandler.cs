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
                         .Include(b => b.Items)
                         .FirstOrDefaultAsync(cancellationToken)
                     ?? await CreateBasketAsync(cancellationToken);

        var basketItem = basket.Items
            .FirstOrDefault(bi => bi.ProductId == request.ProductId);

        if (basketItem == null)
        {
            await CreateBasketItemAsync(request, cancellationToken, basket);
        }
        else
        {
            basketItem.Quantity += request.Quantity;
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task CreateBasketItemAsync(AddBasketItemCommand request,
        CancellationToken cancellationToken, Basket basket)
    {
        var newBasketItem = new BasketItem
        {
            BasketId = basket.Id,
            ProductId = request.ProductId,
            Quantity = request.Quantity
        };

        await _dbContext.BasketItems.AddAsync(newBasketItem, cancellationToken);
    }

    private async Task<Basket> CreateBasketAsync(CancellationToken cancellationToken)
    {
        var createBasketCommand = new CreateBasketCommand
        {
            UserId = _userAccessor.UserId
        };

        return await _mediator.Send(createBasketCommand, cancellationToken);
    }
}