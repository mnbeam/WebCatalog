using MediatR;
using Microsoft.EntityFrameworkCore;
using WebCatalog.Domain.Entities.BasketEntities;
using WebCatalog.Domain.Entities.OrderEntities;
using WebCatalog.Logic.Common.Configurations;
using WebCatalog.Logic.Common.Exceptions;
using WebCatalog.Logic.Common.ExternalServices;

namespace WebCatalog.Logic.WebCatalog.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand>
{
    private readonly AppDbContext _dbContext;
    private readonly IUserAccessor _userAccessor;

    public CreateOrderCommandHandler(AppDbContext dbContext,
        IUserAccessor userAccessor)
    {
        _dbContext = dbContext;
        _userAccessor = userAccessor;
    }

    public async Task Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var basket = await _dbContext.Baskets
                         .Include(b => b.BasketItems)
                         .Where(b => b.UserId == _userAccessor.UserId)
                         .FirstOrDefaultAsync(cancellationToken)
                     ?? throw new WebCatalogNotFoundException(nameof(Basket), _userAccessor.UserId);

        if (basket.BasketItems == null)
        {
            throw new WebCatalogEmptyBasketException();
        }

        var basketItemsIds =
            basket.BasketItems.Select(basketItem => basketItem.ProductId).ToArray();

        var catalogProducts = await _dbContext.Products
            .Where(product => basketItemsIds.Contains(product.Id))
            .ToListAsync(cancellationToken);

        var orderItems = basket.BasketItems.Select(basketItem =>
        {
            var item = catalogProducts.First(p => p.Id == basketItem.ProductId);

            var orderedItem = new ProductOrdered
            {
                ProductId = item.Id,
                ProductName = item.Name
            };

            var orderItem = new OrderItem
            {
                ProductOrdered = orderedItem,
                UnitPrice = item.Price,
                Units = basketItem.Quantity
            };

            return orderItem;
        }).ToList();

        var order = new Order
        {
            UserId = _userAccessor.UserId,
            OrderItems = orderItems
        };

        _dbContext.Add(order);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}