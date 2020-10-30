using AutoMapper;
using Order.Api.Models;
using Order.Data;

namespace Order.Api.Infrastructure.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<OrderModel, Entity.Order>()
                .ForMember(a => a.OrderStatus, b => b.MapFrom(c => (int)c.OrderStatus))
                .ForMember(a => a.PaymentStatus, b => b.MapFrom(c => (int)c.PaymentStatus))
                .ForMember(a => a.PaymentType, b => b.MapFrom(c => (int)c.PaymentType));
            CreateMap<OrderItemModel, Entity.OrderItem>();

            CreateMap<Entity.Order, OrderModel>()
                .ForMember(a => a.OrderStatus, b => b.MapFrom(c => (OrderStatus)c.OrderStatus))
                .ForMember(a => a.PaymentStatus, b => b.MapFrom(c => (PaymentStatus)c.PaymentStatus))
                .ForMember(a => a.PaymentType, b => b.MapFrom(c => (PaymentType)c.PaymentType));
            CreateMap<Entity.OrderItem, OrderItemModel>();
        }
    }
}
