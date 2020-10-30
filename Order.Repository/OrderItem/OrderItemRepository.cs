using Microsoft.EntityFrameworkCore;
using Order.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.Repository
{
    public class OrderItemRepository : BaseRepository, IOrderItemRepository
    {
        public async Task<List<Entity.OrderItem>> GetByOrderAsync(int orderId)
        {
            using (var context = CreateOrderContext())
            {
                return await context.OrderItems.Where(a => a.OrderId == orderId).ToListAsync();
            }
        }

        public async Task<Entity.OrderItem> GetByIdAsync(int id)
        {
            using (var context = CreateOrderContext())
            {
                return await context.OrderItems.FirstOrDefaultAsync(a => a.Id == id);
            }
        }

        public async Task<OrderSaveResponse> SaveAsync(OrderSaveRequest<Entity.OrderItem> request)
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

        public async Task<OrderSaveResponse> DeleteAsync(Entity.OrderItem entity)
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
