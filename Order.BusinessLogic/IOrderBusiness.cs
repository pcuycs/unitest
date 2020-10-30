using Order.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Order.BusinessLogic
{
    public interface IOrderBusiness
    {
        Task<List<Entity.Order>> GetAllAsync();

        Task<Entity.Order> GetAsync(string orderNumber);

        Task<OrderSaveResponse> CreateAsync(OrderSaveRequest<Entity.Order> request);

        Task<OrderSaveResponse> ProcessAsync(string orderNumber);

        Task<OrderSaveResponse> DeliveryAsync(string orderNumber);

        Task<OrderSaveResponse> CompleteAsync(string orderNumber);

        Task<OrderSaveResponse> CancelAsync(string orderNumber);
    }
}
