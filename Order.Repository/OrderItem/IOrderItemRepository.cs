using Order.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Order.Repository
{
    public interface IOrderItemRepository
    {
        Task<List<Entity.OrderItem>> GetByOrderAsync(int orderId);

        Task<Entity.OrderItem> GetByIdAsync(int id);

        Task<OrderSaveResponse> SaveAsync(OrderSaveRequest<Entity.OrderItem> request);

        Task<OrderSaveResponse> DeleteAsync(Entity.OrderItem entity);
    }
}
