﻿using MediatR;

namespace WebCatalog.Logic.WebCatalog.Orders.Queries.GetOrderList;

public class GetOrdersCommand : IRequest<OrderListVm>
{
}