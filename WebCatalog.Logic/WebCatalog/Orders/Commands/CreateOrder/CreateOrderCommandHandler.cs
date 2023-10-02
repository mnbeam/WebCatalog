using MediatR;
using Microsoft.EntityFrameworkCore;
using WebCatalog.Domain.Entities.BasketEntities;
using WebCatalog.Domain.Entities.OrderEntities;
using WebCatalog.Domain.Entities.ProductEntities;
using WebCatalog.Logic.Common.Configurations;
using WebCatalog.Logic.Common.Exceptions;
using WebCatalog.Logic.Common.ExternalServices;
using WebCatalog.Logic.Common.ExternalServices.Email;

namespace WebCatalog.Logic.WebCatalog.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand>
{
    private readonly IDateTimeService _dateTimeService;
    private readonly AppDbContext _dbContext;
    private readonly IEmailService _emailService;
    private readonly IUserAccessor _userAccessor;

    public CreateOrderCommandHandler(AppDbContext dbContext,
        IUserAccessor userAccessor,
        IDateTimeService dateTimeService,
        IEmailService emailService)
    {
        _dbContext = dbContext;
        _userAccessor = userAccessor;
        _dateTimeService = dateTimeService;
        _emailService = emailService;
    }

    public async Task Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var basket = await _dbContext.Baskets
                         .Include(b => b.Items)
                         .Where(b => b.UserId == _userAccessor.UserId)
                         .FirstOrDefaultAsync(cancellationToken)
                     ?? throw new WebCatalogNotFoundException(nameof(Basket), _userAccessor.UserId);

        var orderItems = await GetOrderItemsAsync(basket, cancellationToken);

        var order = new Order
        {
            UserId = _userAccessor.UserId,
            OrderItems = orderItems,
            OrderDate = _dateTimeService.Now
        };

        _dbContext.Orders.Add(order);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var messageDto = new EmailMessageDto($"Order {order.Id}", "oorzhak.iv@gmail.com",
            "Thanks for order!");
        await _emailService.SendAsync(messageDto);
    }

    private async Task<List<OrderItem>> GetOrderItemsAsync(Basket basket,
        CancellationToken cancellationToken)
    {
        if (basket.Items == null)
        {
            throw new WebCatalogEmptyBasketException();
        }

        var catalogProducts = await GetActualCatalogProducts(basket, cancellationToken);

        var orderItems = basket.Items
            .Select(basketItem => GetActualOrderItem(catalogProducts, basketItem))
            .ToList();

        return orderItems;
    }

    private async Task<List<Product>> GetActualCatalogProducts(Basket basket,
        CancellationToken cancellationToken)
    {
        var basketItemsIds = basket.Items
            .Select(basketItem => basketItem.ProductId)
            .ToArray();

        var catalogProducts = await _dbContext.Products
            .Where(product => basketItemsIds.Contains(product.Id))
            .ToListAsync(cancellationToken);
        return catalogProducts;
    }

    private static OrderItem GetActualOrderItem(List<Product> catalogProducts,
        BasketItem basketItem)
    {
        var item = catalogProducts.First(p => p.Id == basketItem.ProductId);

        var orderedItem = new ProductOrdered
        {
            ProductId = item.Id,
            ProductName = item.Name
        };

        return new OrderItem
        {
            ProductOrdered = orderedItem,
            UnitPrice = item.Price,
            Units = basketItem.Quantity
        };
    }
}