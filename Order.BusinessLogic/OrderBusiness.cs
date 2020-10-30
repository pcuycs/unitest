using Order.Data;
using Order.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Order.BusinessLogic
{
    public class OrderBusiness : IOrderBusiness
    {
        private readonly IOrderRepository _orderRepository;

        public OrderBusiness(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public Task<List<Entity.Order>> GetAllAsync()
        {
            return _orderRepository.GetAllAsync();
        }

        public Task<Entity.Order> GetAsync(string orderNumber)
        {
            return _orderRepository.GetByOrderNumberAsync(orderNumber);
        }

        public async Task<OrderSaveResponse> CreateAsync(OrderSaveRequest<Entity.Order> request)
        {
            if (!CanCreateOrder(request.Entity))
            {
                return new OrderSaveResponse
                {
                    Success = false,
                    Message = "Đơn hàng không đủ điều kiện để tạo"
                };
            }

            return await _orderRepository.SaveAsync(request);
        }

        public async Task<OrderSaveResponse> ProcessAsync(string orderNumber)
        {
            var entity = await _orderRepository.GetByOrderNumberAsync(orderNumber);
            if (entity == null)
            {
                return new OrderSaveResponse
                {
                    Success = false,
                    Message = "Đơn hàng không tồn tại"
                };
            }

            if (!CanCreateOrder(entity))
            {
                return new OrderSaveResponse
                {
                    Success = false,
                    Message = "Đơn hàng không đủ điều kiện để hủy"
                };
            }

            entity.OrderStatus = (int)OrderStatus.Proccessing;

            return await _orderRepository.SaveAsync(new OrderSaveRequest<Entity.Order>
            {
                Entity = entity,
                UpdatedDate = DateTime.Now
            });
        }

        public async Task<OrderSaveResponse> DeliveryAsync(string orderNumber)
        {
            var entity = await _orderRepository.GetByOrderNumberAsync(orderNumber);
            if (entity == null)
            {
                return new OrderSaveResponse
                {
                    Success = false,
                    Message = "Đơn hàng không tồn tại"
                };
            }

            if (!CanCancelOrder(entity))
            {
                return new OrderSaveResponse
                {
                    Success = false,
                    Message = "Đơn hàng không đủ điều kiện để hủy"
                };
            }

            entity.OrderStatus = (int)OrderStatus.Proccessing;

            return await _orderRepository.SaveAsync(new OrderSaveRequest<Entity.Order>
            {
                Entity = entity,
                UpdatedDate = DateTime.Now
            });
        }

        public async Task<OrderSaveResponse> CompleteAsync(string orderNumber)
        {
            var entity = await _orderRepository.GetByOrderNumberAsync(orderNumber);
            if (entity == null)
            {
                return new OrderSaveResponse
                {
                    Success = false,
                    Message = "Đơn hàng không tồn tại"
                };
            }

            if (!CanCancelOrder(entity))
            {
                return new OrderSaveResponse
                {
                    Success = false,
                    Message = "Đơn hàng không đủ điều kiện để hủy"
                };
            }

            entity.OrderStatus = (int)OrderStatus.Completed;

            return await _orderRepository.SaveAsync(new OrderSaveRequest<Entity.Order>
            {
                Entity = entity,
                UpdatedDate = DateTime.Now
            });
        }

        public async Task<OrderSaveResponse> CancelAsync(string orderNumber)
        {
            var entity = await _orderRepository.GetByOrderNumberAsync(orderNumber);
            if (entity == null)
            {
                return new OrderSaveResponse
                {
                    Success = false,
                    Message = "Đơn hàng không tồn tại"
                };
            }

            if (!CanCancelOrder(entity))
            {
                return new OrderSaveResponse
                {
                    Success = false,
                    Message = "Đơn hàng không đủ điều kiện để hủy"
                };
            }

            entity.OrderStatus = (int)OrderStatus.Cancelled;

            return await _orderRepository.SaveAsync(new OrderSaveRequest<Entity.Order>
            {
                Entity = entity,
                UpdatedDate = DateTime.Now
            });


        }

        public bool CanCreateOrder(Entity.Order order)
        {
            return order.OrderStatus == (int)OrderStatus.New &&
                (order.PaymentStatus == (int)PaymentStatus.NotPaid ||
                order.PaymentStatus == (int)PaymentStatus.Paid);
        }

        public bool CanDeliveryOrder(Entity.Order order)
        {
            return order.OrderStatus == (int)OrderStatus.Proccessing &&
                (order.PaymentStatus == (int)PaymentStatus.NotPaid ||
                order.PaymentStatus == (int)PaymentStatus.Paid);
        }

        public bool CanCompleteOrder(Entity.Order order)
        {
            return order.OrderStatus == (int)OrderStatus.Shipping &&
                order.PaymentStatus == (int)PaymentStatus.Paid;
        }

        public bool CanCancelOrder(Entity.Order order)
        {
            return (order.PaymentStatus == (int)PaymentStatus.NotPaid &&
                (order.OrderStatus == (int)OrderStatus.New ||
                order.OrderStatus == (int)OrderStatus.Proccessing)) ||
                (order.PaymentStatus == (int)PaymentStatus.Paid &&
                (order.OrderStatus == (int)OrderStatus.New ||
                order.OrderStatus == (int)OrderStatus.Proccessing ||
                order.OrderStatus == (int)OrderStatus.Shipping));
        }

        public bool CanRefundOrder(Entity.Order order)
        {
            return order.PaymentStatus == (int)PaymentStatus.Paid;
        }
    }
}
