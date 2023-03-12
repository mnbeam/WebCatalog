using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebCatalog.Logic.Common.Configurations;
using WebCatalog.Logic.Common.ExternalServices;
using WebCatalog.Logic.WebCatalog.Baskets.Commands.CreateBasket;

namespace WebCatalog.Logic.WebCatalog.Baskets.Queries.GetBasket;

public class GetBasketQueryHandler : IRequestHandler<GetBasketQuery, BasketVm>
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IUserAccessor _userAccessor;

    public GetBasketQueryHandler(AppDbContext dbContext,
        IMapper mapper,
        IUserAccessor userAccessor,
        IMediator mediator)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _userAccessor = userAccessor;
        _mediator = mediator;
    }

    public async Task<BasketVm> Handle(GetBasketQuery request, CancellationToken cancellationToken)
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

        return new BasketVm
        {
            BasketItems = await _dbContext.BasketItems
                .Where(bi => bi.BasketId == basket.Id)
                .ProjectTo<BasketItemVm>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken)
        };
    }
}