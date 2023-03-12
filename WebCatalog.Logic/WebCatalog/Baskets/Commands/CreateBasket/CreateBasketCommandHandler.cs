using MediatR;
using WebCatalog.Domain.Entities.BasketEntities;
using WebCatalog.Logic.Common.ExternalServices;

namespace WebCatalog.Logic.WebCatalog.Baskets.Commands.CreateBasket;

internal class CreateBasketCommandHandler : IRequestHandler<CreateBasketCommand, Basket>
{
    private readonly AppDbContext _dbContext;

    public CreateBasketCommandHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Basket> Handle(CreateBasketCommand request,
        CancellationToken cancellationToken)
    {
        var basket = new Basket
        {
            UserId = request.UserId
        };

        await _dbContext.Baskets.AddAsync(basket, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return basket;
    }
}