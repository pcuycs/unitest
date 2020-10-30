using Microsoft.EntityFrameworkCore;
using Order.Entity.Context;

namespace Order.Repository
{
    public abstract class BaseRepository
    {
        protected OrderContext CreateOrderContext()
        {
            var options = new DbContextOptionsBuilder<OrderContext>()
               .UseInMemoryDatabase(databaseName: "OrderDb")
               .Options;

            return new OrderContext(options);
        }
    }
}
