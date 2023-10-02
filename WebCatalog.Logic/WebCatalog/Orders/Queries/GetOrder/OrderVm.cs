using AutoMapper;
using WebCatalog.Domain.Entities.OrderEntities;
using WebCatalog.Logic.Common.Mappings;

namespace WebCatalog.Logic.WebCatalog.Orders.Queries.GetOrder;

public class OrderVm : IMapWith<Order>
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public DateTimeOffset OrderDate { get; set; }

    public List<OrderItem> OrderItems { get; set; } = new();

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Order, OrderVm>()
            .ForMember(orderVm => orderVm.OrderItems,
                opt => opt.MapFrom(o => o.OrderItems));
    }
}