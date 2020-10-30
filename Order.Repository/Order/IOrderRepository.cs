using Order.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Order.Repository
{
    public interface IOrderRepository
    {
        Task<List<Entity.Order>> GetAllAsync();

        Task<Entity.Order> GetByIdAsync(int id);

        Task<Entity.Order> GetByOrderNumberAsync(string orderNumber);

        Task<OrderSaveResponse> SaveAsync(OrderSaveRequest<Entity.Order> request);

        Task<OrderSaveResponse> DeleteAsync(Entity.Order entity);
    }
}
