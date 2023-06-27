using MediatR;
using Microsoft.EntityFrameworkCore;
using WebCatalog.Domain.Entities.ProductEntities;
using WebCatalog.Logic.Common.Configurations;
using WebCatalog.Logic.Common.Exceptions;
using WebCatalog.Logic.Common.ExternalServices;

namespace WebCatalog.Logic.WebCatalog.Baskets.Commands.RemoveBasketItem;

public class RemoveBasketItemCommandHandler : IRequestHandler<RemoveBasketItemCommand>
{
    private readonly AppDbContext _dbContext;
    private readonly IUserAccessor _userAccessor;

    public RemoveBasketItemCommandHandler(AppDbContext dbContext,
        IUserAccessor userAccessor)
    {
        _dbContext = dbContext;
        _userAccessor = userAccessor;
    }

    public async Task Handle(RemoveBasketItemCommand request, CancellationToken cancellationToken)
    {
        var basket = await _dbContext.Baskets
                         .Include(b => b.Items)
                         .FirstOrDefaultAsync(b => b.UserId == _userAccessor.UserId,
                             cancellationToken)
                     ?? throw new WebCatalogEmptyBasketException();

        var basketItem = basket.Items.FirstOrDefault(bi => bi.ProductId == request.ProductId)
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