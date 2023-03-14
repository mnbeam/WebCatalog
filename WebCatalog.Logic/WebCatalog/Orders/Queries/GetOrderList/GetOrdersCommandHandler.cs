using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebCatalog.Logic.Common.Configurations;
using WebCatalog.Logic.Common.ExternalServices;
using WebCatalog.Logic.WebCatalog.Orders.Queries.GetOrder;

namespace WebCatalog.Logic.WebCatalog.Orders.Queries.GetOrderList;

public class GetOrdersCommandHandler : IRequestHandler<GetOrdersCommand, OrderListVm>
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IUserAccessor _userAccessor;

    public GetOrdersCommandHandler(AppDbContext dbContext,
        IUserAccessor userAccessor,
        IMapper mapper)
    {
        _dbContext = dbContext;
        _userAccessor = userAccessor;
        _mapper = mapper;
    }

    public async Task<OrderListVm> Handle(GetOrdersCommand request,
        CancellationToken cancellationToken)
    {
        return new OrderListVm
        {
            Orders = await _dbContext.Orders
                .Where(o => o.UserId == _userAccessor.UserId)
                .ProjectTo<OrderVm>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken)
        };
    }
}