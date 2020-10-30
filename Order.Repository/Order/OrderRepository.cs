using Microsoft.EntityFrameworkCore;
using Order.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Order.Repository
{
    public class OrderRepository : BaseRepository, IOrderRepository
    {
        public async Task<List<Entity.Order>> GetAllAsync()
        {
            using (var context = CreateOrderContext())
            {
                return await context.Orders.ToListAsync();
            }
        }

        public async Task<Entity.Order> GetByIdAsync(int id)
        {
            using (var context = CreateOrderContext())
            {
                return await context.Orders.FirstOrDefaultAsync(a => a.Id == id);
            }
        }

        public async Task<Entity.Order> GetByOrderNumberAsync(string orderNumber)
        {
            using (var context = CreateOrderContext())
            {
                return await context.Orders.FirstOrDefaultAsync(a => a.OrderNumber == orderNumber);
            }
        }

        public async Task<OrderSaveResponse> SaveAsync(OrderSaveRequest<Entity.Order> request)
        {
            using (var context = CreateOrderContext())
            {
                context.Entry(request.Entity).State = request.IsEdit ? EntityState.Modified : EntityState.Added;

                var result = await context.SaveChangesAsync();

                return new OrderSaveResponse
                {
                    Success = result > 0,
                    Id = request.Entity.Id
                };
            }
        }

        public async Task<OrderSaveResponse> DeleteAsync(Entity.Order entity)
        {
            using (var context = CreateOrderContext())
            {
                context.Entry(entity).State = EntityState.Deleted;

                var result = await context.SaveChangesAsync();

                return new OrderSaveResponse
                {
                    Success = result > 0
                };
            }
        }
    }
}
