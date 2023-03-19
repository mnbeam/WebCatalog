using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebCatalog.Domain.Entities.OrderEntities;
using WebCatalog.Logic.Common.Configurations;
using WebCatalog.Logic.Common.Exceptions;
using WebCatalog.Logic.Common.ExternalServices;

namespace WebCatalog.Logic.WebCatalog.Orders.Queries.GetOrder;

public class GetOrderCommandHandler : IRequestHandler<GetOrderCommand, OrderVm>
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IUserAccessor _userAccessor;

    public GetOrderCommandHandler(AppDbContext dbContext,
        IUserAccessor userAccessor,
        IMapper mapper)
    {
        _dbContext = dbContext;
        _userAccessor = userAccessor;
        _mapper = mapper;
    }

    public async Task<OrderVm> Handle(GetOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _dbContext.Orders
            .Where(o => o.UserId == _userAccessor.UserId &&
                        o.Id == request.OrderId)
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(cancellationToken);

        if (order == null)
        {
            throw new WebCatalogNotFoundException(nameof(Order), request.OrderId);
        }

        return _mapper.Map<OrderVm>(order);
    }
}